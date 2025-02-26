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
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace CarsStorage.BLL.Services.Services
{
	/// <summary>
	/// Класс сервиса для аутентификации пользователей.
	/// </summary>
	/// <param name="usersRepository">Репозиторий пользователей.</param>
	/// <param name="tokenService">Сервис токенов.</param>
	/// <param name="mapper">Объект меппера.</param>
	/// <param name="passwordHasher">Объект для хеширования паролей.</param>
	/// <param name="logger">Объект для выполнения логирования.</param>
	public class AuthenticateService(IUsersRepository usersRepository,  ITokensService tokenService, IMapper mapper, IPasswordHasher passwordHasher, ILogger<AuthenticateService> logger) : IAuthenticateService
	{
		/// <summary>
		/// Метод для входа пользователя в приложение.
		/// </summary>
		/// <param name="userLoginDTO">Объект, представляющий данные пользователя для входа в приложение.</param>
		/// <returns>Объект токена доступа.</returns>
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
				return new ServiceResult<JWTTokenDTO>(await GetJWTToken(mapper.Map<UserDTO>(userEntity)));
			}
			catch (Exception exception)
			{
				logger.LogError("Ошибка в {service} в {method} при входе пользователя в приложение: {errorMessage}", this, nameof(this.LogIn), exception.Message);
				throw new BadRequestException(exception.Message);
			}
		}


		/// <summary>
		/// Метод создает объект пользователя по данным от стороннего провайдера аутентификации, если пользователь не был зарегистрирован.
		/// </summary>
		/// <param name="authUserDataDTO">Объект, представляющий данные об аутентифицированном пользователе.</param>
		/// <returns>Объект пользователя.</returns>
		public async Task<ServiceResult<UserDTO>> CreateAuthUserIfNotExist(AuthUserDataDTO authUserDataDTO)
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
				return new ServiceResult<UserDTO>(mapper.Map<UserDTO>(userEntity));
			}
			catch (Exception exception)
			{
				logger.LogError("Ошибка в {service} в {method} при создании пользователя после сторонней аутентификации: {errorMessage}", this, nameof(this.LogInAuthUser), exception.Message);
				throw new BadRequestException(exception.Message);
			}
		}



		/// <summary>
		/// Метод для входа в приложение пользователя, аутентифицированного на стороннем провайдере аутентификации.
		/// </summary>
		/// <param name="userDTO">Объект аутентифицированного пользователя.</param>
		/// <returns>Объект токена доступа.</returns>
		public async Task<ServiceResult<JWTTokenDTO>> LogInAuthUser(UserDTO userDTO)
		{
			try
			{				
				var jwtTokenDTO = await GetJWTToken(userDTO);
				var updateTokenResult = await tokenService.UpdateToken(userDTO.Id, jwtTokenDTO);
				if (!updateTokenResult.IsSuccess)
					throw updateTokenResult.ServiceError;
				return new ServiceResult<JWTTokenDTO>(updateTokenResult.Result);
			}
			catch (Exception exception)
			{
				logger.LogError("Ошибка в {service} в {method} при входе аутентифицированного пользователя в приложение: {errorMessage}", this, nameof(this.LogInAuthUser), exception.Message);
				throw new BadRequestException(exception.Message);
			}
		}


		/// <summary>
		/// Метод для обновления refresh токена.
		/// </summary>
		/// <param name="jwtTokenDTO">Объект токена.</param>
		/// <returns>Объект токена с обновленным refresh токеном.</returns>
		public async Task<ServiceResult<JWTTokenDTO>> RefreshToken(JWTTokenDTO jwtTokenDTO)
		{
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
				logger.LogError("Ошибка в {service} в {method} при обновлении токена доступа: {errorMessage}", this, nameof(this.RefreshToken), exception.Message);
				throw new BadRequestException(exception.Message);
			}
		}


		/// <summary>
		/// Метод сервиса для выхода пользователя из приложения.
		/// </summary>
		/// <param name="accessToken">Строка токена доступа.</param>
		/// <returns>Идентификатор пользователя, вышедшего из приложения.</returns>
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
				logger.LogError("Ошибка в {service} в {method} при выходе пользователя из приложения: {errorMessage}", this, nameof(this.LogOut), exception.Message);
				throw new BadRequestException(exception.Message);
			}
		}


		/// <summary>
		/// Закрытый метод сервиса для получения объекта токена доступа.
		/// </summary>
		/// <param name="userDTO">Объект пользователя.</param>
		/// <returns>Объект токена для входа пользователя в приложение.</returns>
		private async Task<JWTTokenDTO> GetJWTToken(UserDTO userDTO)
		{
			try
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
			catch (Exception exception)
			{
				logger.LogError("Ошибка в {service} в {method} при получении объекта токена доступа: {errorMessage}", this, nameof(this.GetJWTToken), exception.Message);
				throw new BadRequestException(exception.Message);
			}
		}
	}
}
