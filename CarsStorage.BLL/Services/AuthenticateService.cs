using AutoMapper;
using CarsStorage.BLL.Abstractions.Exceptions;
using CarsStorage.BLL.Abstractions.General;
using CarsStorage.BLL.Abstractions.ModelsDTO;
using CarsStorage.BLL.Abstractions.ModelsDTO.Token;
using CarsStorage.BLL.Abstractions.ModelsDTO.User;
using CarsStorage.BLL.Abstractions.Repositories;
using CarsStorage.BLL.Abstractions.Services;
using CarsStorage.BLL.Implementations.Config;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace CarsStorage.BLL.Implementations.Services
{
	/// <summary>
	/// Сервис для аутентификации.
	/// </summary>
	public class AuthenticateService(IUsersRepository usersRepository, IRolesService rolesService, ITokensService tokenService, 
		IMapper mapper, IOptions<InitialConfig> initialConfig) : IAuthenticateService
	{
		/// <summary>
		/// Метод сервиса для регистрации пользователя в приложении.
		/// </summary>
		public async Task<ServiceResult<UserDTO>> Register(UserRegisterDTO userRegisterDTO)
		{
			try
			{			
				var userCreaterDTO = mapper.Map<UserCreaterDTO>(userRegisterDTO);
				var rolesNames = new List<string>([initialConfig.Value.DefaultRoleName]);
				var userDTO = await usersRepository.Register(userCreaterDTO, rolesNames);
				return new ServiceResult<UserDTO>(userDTO, null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<UserDTO>(null, new BadRequestException(exception.Message));
			}
		}


		/// <summary>
		/// Метод сервиса для входа пользователя в приложении и возврата токена клиенту.
		/// </summary>
		public async Task<ServiceResult<JWTTokenDTO>> LogIn(UserLoginDTO userLoginDTO) 
		{
			try
			{
				var userDTO = await usersRepository.GetUserIfValid(userLoginDTO);			

				var roleDTOList = userDTO.RolesList;
				var roleClaims = roleDTOList.SelectMany(role => role.RoleClaims).Distinct().ToList();
				var userClaims = new List<Claim> { new(ClaimTypes.Name, userDTO.UserName) };

				roleClaims.ForEach(roleClaim => userClaims.Add(new Claim(typeof(RoleClaimTypeBLL).ToString(), roleClaim.ToString())));				
				
				var accessTokenFromService = await tokenService.GetAccessToken(userClaims);
				var refreshTokenFromService = await tokenService.GetRefreshToken();

				var jwtTokenDTO = new JWTTokenDTO()
				{
					AccessToken = accessTokenFromService.Result,
					RefreshToken = refreshTokenFromService.Result
				};

				jwtTokenDTO = await usersRepository.UpdateToken(userDTO.Id, jwtTokenDTO);
				return new ServiceResult<JWTTokenDTO>(jwtTokenDTO, null);				
			}
			catch (Exception exception)
			{
				return new ServiceResult<JWTTokenDTO>(null, new UnauthorizedAccessException(exception.Message));
			}
		}


		/// <summary>
		/// Метод сервиса возвращает как результат объект токена с обновленным refresh токеном.
		/// </summary>
		public async Task<ServiceResult<JWTTokenDTO>> RefreshToken(JWTTokenDTO jwtTokenDTO)
		{
			//по refresh токену нашли userDTO, по userId возвращаем истекший access токен,
			//из кот. получаем список клаймов из ClaimsPrincipal для генерации нового access токена,
			//refreshToken - просто обновляется рандомная строка (без полезной нагрузки)
			try
			{
				var userDTO = await usersRepository.GetUserByRefreshToken(jwtTokenDTO.RefreshToken);
				jwtTokenDTO = await usersRepository.GetTokenByUserId(userDTO.Id);
				var principal = await tokenService.GetClaimsPrincipalFromExperedToken(jwtTokenDTO.AccessToken);

				var accessToken = await tokenService.GetAccessToken(principal.Result.Claims.ToList());
				var refreshToken = await tokenService.GetRefreshToken();

				var newRefreshingToken = new JWTTokenDTO()
				{
					AccessToken = accessToken.Result,
					RefreshToken = refreshToken.Result
				};
				await usersRepository.UpdateToken(userDTO.Id, newRefreshingToken);
				return new ServiceResult<JWTTokenDTO>(newRefreshingToken, null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<JWTTokenDTO>(null, new UnauthorizedAccessException(exception.Message));
			}
		}


		/// <summary>
		/// Метод сервиса для выхода пользователя из приложения, возвращает как результат id вышедшего пользователя.
		/// </summary>
		public async Task<ServiceResult<int>> LogOut(string accessToken) 
		{
			try
			{
				var userId = await usersRepository.ClearToken(accessToken);
				return new ServiceResult<int>(userId, null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<int>(0, new BadRequestException(exception.Message));
			}
		}
	}
}
