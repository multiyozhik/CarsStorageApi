using AutoMapper;
using CarsStorage.BLL.Abstractions.Exceptions;
using CarsStorage.BLL.Abstractions.General;
using CarsStorage.BLL.Abstractions.Services;
using CarsStorage.BLL.Abstractions.ModelsDTO.Token;
using CarsStorage.BLL.Abstractions.ModelsDTO.User;
using CarsStorage.BLL.Implementations.Config;
using Microsoft.Extensions.Options;
using CarsStorage.BLL.Abstractions.Repositories;

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
				userCreaterDTO.RolesList = (await rolesService.GetRolesByNamesList(rolesNames)).Result;
				var userDTO = await usersRepository.Create(userCreaterDTO);
				return new ServiceResult<UserDTO>(userDTO, null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<UserDTO>(null, new BadRequestException(exception.Message));
			}
		}


		/// <summary>
		/// Метод сервиса для входа пользователя в приложении.
		/// </summary>
		public async Task<ServiceResult<JWTTokenDTO>> LogIn(UserLoginDTO userLoginDTO) 
		{
			try
			{
				var userDTO = await usersRepository.GetUserIfValid(userLoginDTO);
				var claimsList = await rolesService.GetClaimsByUser(userDTO);

				var accessTokenFromService = await tokenService.GetAccessToken(claimsList.Result);
				var refreshTokenFromService = await tokenService.GetRefreshToken();

				var jwtTokenDTO = new JWTTokenDTO()
				{
					AccessToken = accessTokenFromService.Result,
					RefreshToken = refreshTokenFromService.Result
				};

				await usersRepository.UpdateToken(userDTO.Id, jwtTokenDTO);
				return new ServiceResult<JWTTokenDTO>(jwtTokenDTO, null);				
			}
			catch (Exception exception)
			{
				return new ServiceResult<JWTTokenDTO>(null, new UnauthorizedAccessException(exception.Message));
			}
		}

		/// <summary>
		/// Метод сервиса возвращает как результат обновленный объект токена.
		/// </summary>
		public async Task<ServiceResult<JWTTokenDTO>> RefreshToken(JWTTokenDTO jwtTokenDTO)
		{
			//по refresh токену нашли userDTO => userId => недействительный access токен,
			//из кот. получили список клаймов из ClaimsPrincipal для генерации нового access токена,
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
		public async Task<ServiceResult<int>> LogOut(JWTTokenDTO jwtTokenDTO) 
		{
			try
			{
				var userId = await usersRepository.ClearToken(jwtTokenDTO.RefreshToken);
				return new ServiceResult<int>(userId, null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<int>(int.MinValue, new BadRequestException(exception.Message));
			}
		}
	}
}
