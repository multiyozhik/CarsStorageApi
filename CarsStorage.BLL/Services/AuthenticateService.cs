using AutoMapper;
using CarsStorage.Abstractions.BLL.Services;
using CarsStorage.Abstractions.DAL.Repositories;
using CarsStorage.Abstractions.Exceptions;
using CarsStorage.Abstractions.General;
using CarsStorage.Abstractions.ModelsDTO;
using CarsStorage.Abstractions.ModelsDTO.Token;
using CarsStorage.Abstractions.ModelsDTO.User;
using CarsStorage.BLL.Abstractions.ModelsDTO.User;
using CarsStorage.BLL.Services.Utils;
using CarsStorage.DAL.Entities;
using System.Security.Claims;

namespace CarsStorage.BLL.Services.Services
{
	/// <summary>
	/// Сервис для аутентификации.
	/// </summary>
	public class AuthenticateService(IUsersRepository usersRepository,  ITokensService tokenService, IMapper mapper, IPasswordHasher passwordHasher) : IAuthenticateService
	{
		/// <summary>
		/// Метод сервиса для входа пользователя в приложение и возврата токена клиенту.
		/// </summary>
		public async Task<ServiceResult<JWTTokenDTO>> LogIn(UserLoginDTO userLoginDTO) 
		{
			try
			{
				var userEntity = await usersRepository.GetUserByUserName(userLoginDTO.UserName)
					?? throw new Exception("Неверный логин.");
				if (string.IsNullOrEmpty(userEntity.Hash) || string.IsNullOrEmpty(userEntity.Salt))
					throw new Exception("Не определены пароль и соль.");
				if (!passwordHasher.VerifyPassword(userLoginDTO.Password, userEntity.Hash, userEntity.Salt))
					throw new Exception("Неверный пароль.");
				return new ServiceResult<JWTTokenDTO>(await GetJWTTokenDTO(mapper.Map<UserDTO>(userEntity)));
			}
			catch (Exception exception)
			{
				return new ServiceResult<JWTTokenDTO>(new UnauthorizedAccessException(exception.Message));
			}
		}


		/// <summary>
		/// Метод сервиса для входа аутентифицированного пользователя в приложение.
		/// </summary>
		public async Task<ServiceResult<JWTTokenDTO>> LogInAuthUser(AuthUserDataDTO authUserDataDTO)
		{
			try
			{
				var userEntity = await usersRepository.GetUserByUserName(authUserDataDTO.UserName);
				if (userEntity is null)
				{
					userEntity = new UserEntity()
					{
						UserName = authUserDataDTO.UserName,
						Email = authUserDataDTO.Email,
						RolesList = await usersRepository.GetRolesByRoleNames(authUserDataDTO.RolesNamesList)
					};
					userEntity = await usersRepository.Create(userEntity);
				}
				var userDTO = mapper.Map<UserDTO>(userEntity);
				var jwtTokenDTO = await GetJWTTokenDTO(userDTO);
				var updateTokenResult = await tokenService.UpdateToken(userDTO.Id, jwtTokenDTO);
				if (!updateTokenResult.IsSuccess)
					throw updateTokenResult.ServiceError;
				return new ServiceResult<JWTTokenDTO>(jwtTokenDTO);
			}
			catch (Exception exception)
			{
				return new ServiceResult<JWTTokenDTO>(new NotFoundException(exception.Message));
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
				var userEntity = await usersRepository.GetUserByRefreshToken(jwtTokenDTO.RefreshToken);
				var userDTO = mapper.Map<UserDTO>(userEntity);
				var jwtTokenResult = await tokenService.GetTokenByUserId(userDTO.Id);

				if (!jwtTokenResult.IsSuccess)
					throw jwtTokenResult.ServiceError;

				var principal = tokenService.GetClaimsPrincipalFromExperedToken(jwtTokenResult.Result.AccessToken);

				var accessToken = tokenService.GetAccessToken(principal.Result.Claims.ToList());
				var refreshToken = tokenService.GetRefreshToken();

				var newRefreshingToken = new JWTTokenDTO()
				{
					AccessToken = accessToken.Result,
					RefreshToken = refreshToken.Result
				};
				var refreshTokenResult = await tokenService.UpdateToken(userDTO.Id, newRefreshingToken);
				if (!refreshTokenResult.IsSuccess)
					throw refreshTokenResult.ServiceError;
				return new ServiceResult<JWTTokenDTO>(refreshTokenResult.Result);
			}
			catch (Exception exception)
			{
				return new ServiceResult<JWTTokenDTO>(new UnauthorizedAccessException(exception.Message));
			}
		}


		/// <summary>
		/// Метод сервиса для выхода пользователя из приложения, возвращает как результат id вышедшего пользователя.
		/// </summary>
		public async Task<ServiceResult<int>> LogOut(string accessToken) 
		{
			try
			{
				var userIdResult = await tokenService.ClearToken(accessToken);
				if (!userIdResult.IsSuccess)
					throw userIdResult.ServiceError;
				return new ServiceResult<int>(userIdResult.Result);
			}
			catch (Exception exception)
			{
				return new ServiceResult<int>(new BadRequestException(exception.Message));
			}
		}


		/// <summary>
		/// Закрытый метод сервиса возвращает токен для входа пользователя в приложение.
		/// </summary>
		private async Task<JWTTokenDTO> GetJWTTokenDTO(UserDTO userDTO)
		{
			var roleClaims = userDTO.RolesList.SelectMany(role => role.RoleClaims).Distinct().ToList();
			var userClaims = new List<Claim> { new(ClaimTypes.Name, userDTO.UserName) };

			roleClaims.ForEach(roleClaim => userClaims.Add(new Claim(typeof(RoleClaimTypeBLL).ToString(), roleClaim.ToString())));

			var accessTokenFromService = tokenService.GetAccessToken(userClaims);
			var refreshTokenFromService = tokenService.GetRefreshToken();

			var jwtTokenDTO = new JWTTokenDTO()
			{
				AccessToken = accessTokenFromService.Result,
				RefreshToken = refreshTokenFromService.Result
			};
			var tokenServiceResult = await tokenService.UpdateToken(userDTO.Id, jwtTokenDTO);

			if (!tokenServiceResult.IsSuccess)
				throw tokenServiceResult.ServiceError;
			return tokenServiceResult.Result;
		}
	}
}
