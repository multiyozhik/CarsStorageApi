using AutoMapper;
using CarsStorage.BLL.Abstractions.Exceptions;
using CarsStorage.BLL.Abstractions.Interfaces;
using CarsStorage.BLL.Abstractions.Models;
using CarsStorage.BLL.Abstractions.ModelsDTO.AuthModels;
using CarsStorage.BLL.Abstractions.ModelsDTO.UserDTO;
using CarsStorage.BLL.Repositories.Interfaces;
using CarsStorage.BLL.Repositories.Utils;
using CarsStorage.DAL.Config;
using CarsStorage.DAL.Entities;
using CarsStorage.DAL.Models;
using Microsoft.Extensions.Options;

namespace CarsStorage.BLL.Implementations.Services
{
	/// <summary>
	/// Сервис для аутентификации.
	/// </summary>
	public class AuthenticateService(IUsersRepository usersRepository, IRolesRepository rolesRepository, ITokensService tokenService, 
		IMapper mapper, IOptions<InitialConfig> initialOptions, IPasswordHasher passwordHasher) : IAuthenticateService
	{
		/// <summary>
		/// Метод для регистрации пользователя в приложении.
		/// </summary>
		/// <param name="userRegisterDTO">Объект пользователя с UserName, Email, Password.</param>
		/// <returns></returns>
		public async Task<ServiceResult<UserCreaterWithRolesDTO>> Register(UserRegisterDTO userRegisterDTO)
		{
			try
			{
				var userCreater = mapper.Map<UserCreater>(userRegisterDTO);
				userCreater.Roles = [initialOptions.Value.DefaultRoleName];
				var userRegister = await usersRepository.Create(userCreater);
				return new ServiceResult<UserCreaterWithRolesDTO>(mapper.Map<UserCreaterWithRolesDTO>(userRegister), null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<UserCreaterWithRolesDTO>(null, new BadRequestException(exception.Message));
			}
		}


		/// <summary>
		/// Метод для входа пользователя в приложении.
		/// </summary>
		/// <param name="userLoginDTO">Объект пользователя с UserName и Password.</param>
		/// <returns></returns>
		public async Task<ServiceResult<JWTTokenDTO>> LogIn(UserLoginDTO userLoginDTO) 
		{
			try
			{
				var userEntity = await usersRepository.FindByName(userLoginDTO.UserName);
				if (userEntity is null)
					return new ServiceResult<JWTTokenDTO>(null, new ForbiddenException("Неверный логин."));
				if (!passwordHasher.VerifyPassword(userLoginDTO.Password, userEntity.Hash, userEntity.Salt))
					return new ServiceResult<JWTTokenDTO>(null, new ForbiddenException("Неверный пароль."));

				var claimsList = rolesRepository.GetClaimsByUser(mapper.Map<UserEntity>(userLoginDTO));

				var accessToken = await tokenService.GetAccessToken(claimsList);
				var refreshToken = await tokenService.GetRefreshToken();

				var jwtTokenDTO = new JWTTokenDTO()
				{
					AccessToken = accessToken.Result,
					RefreshToken = refreshToken.Result
				};

				await usersRepository.UpdateToken(userEntity.UserEntityId, jwtTokenDTO);
				return new ServiceResult<JWTTokenDTO>(jwtTokenDTO, null);				
			}
			catch (Exception exception)
			{
				return new ServiceResult<JWTTokenDTO>(null, new UnauthorizedAccessException(exception.Message));
			}
		}

		/// <summary>
		/// Метод возвращает как результат обновленный объект токена.
		/// </summary>
		/// <param name="jwtTokenDTO">Объект токена (токен доступа и токен обновления).</param>
		/// <returns></returns>
		public async Task<ServiceResult<JWTTokenDTO>> RefreshToken(JWTTokenDTO jwtTokenDTO)
		{
			var refreshingToken = jwtTokenDTO;
			try
			{
				var user = await usersRepository.GetUserByRefreshToken(refreshingToken.RefreshToken);
				if (user is null)
					return new ServiceResult<JWTTokenDTO>(null, new BadRequestException("Пользователь с таким refresh_token не найден"));

				var principal = await tokenService.GetClaimsPrincipalFromExperedTokenWithValidation(user.AccessToken);

				var accessToken = await tokenService.GetAccessToken(principal.Result.Claims.ToList());
				var refreshToken = await tokenService.GetRefreshToken();

				refreshingToken = new JWTTokenDTO()
				{
					AccessToken = accessToken.Result,
					RefreshToken = refreshToken.Result
				};
				await usersRepository.UpdateToken(user.UserEntityId, refreshingToken);
				return new ServiceResult<JWTTokenDTO>(refreshingToken, null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<JWTTokenDTO>(null, new UnauthorizedAccessException(exception.Message));
			}
		}


		/// <summary>
		/// Метод для выхода пользователя из приложения, возвращает как результат вышедшего пользователя.
		/// </summary>
		public async Task<ServiceResult<UserDTO>> LogOut(JWTTokenDTO jwtTokenDTO) 
		{
			try
			{
				var user = await usersRepository.ClearToken(jwtTokenDTO);
				return new ServiceResult<UserDTO>(mapper.Map<UserDTO>(user), null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<UserDTO>(null, new BadRequestException(exception.Message));
			}
		}
	}
}
