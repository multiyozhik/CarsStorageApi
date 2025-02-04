using AutoMapper;
using CarsStorage.BLL.Abstractions.Interfaces;
using CarsStorage.BLL.Abstractions.Models;
using CarsStorage.BLL.Abstractions.ModelsDTO.AuthModels;
using CarsStorage.BLL.Abstractions.ModelsDTO.UserDTO;
using CarsStorage.BLL.Repositories.Interfaces;
using CarsStorage.DAL.Config;
using CarsStorage.DAL.Entities;
using CarsStorage.DAL.Models;
using CarsStorage.DAL.Utils;
using Microsoft.Extensions.Options;

namespace CarsStorage.BLL.Implementations.Services
{
	/// <summary>
	/// Сервис для аутентификации.
	/// </summary>
	public class AuthenticateService(IUsersRepository usersRepository, IRolesRepository rolesRepository, ITokensService tokenService, 
		IMapper mapper, IOptions<InitialDbSeedConfig> initialOptions, IPasswordHasher passwordHasher) : IAuthenticateService
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
				return new ServiceResult<UserCreaterWithRolesDTO>(null, exception.Message);
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
				var userEntity = await usersRepository.FindByName(userLoginDTO.UserName)
					?? throw new Exception("Неверный логин.");
				if (!passwordHasher.VerifyPassword(userLoginDTO.Password, userEntity.PasswordHash.hash, userEntity.PasswordHash.salt))
					throw new Exception("Неверный пароль.");

				var claimsList = rolesRepository.GetClaimsByUser(mapper.Map<UserEntity>(userLoginDTO));
				var jwtTokenDTO = new JWTTokenDTO()
				{
					AccessToken = tokenService.GetAccessToken(claimsList, out DateTime accessTokenExpires),
					RefreshToken = tokenService.GetRefreshToken()
				};

				await usersRepository.UpdateToken(userEntity.Id, jwtTokenDTO);
				return new ServiceResult<JWTTokenDTO>(jwtTokenDTO, null);				
			}
			catch (Exception exception)
			{
				return new ServiceResult<JWTTokenDTO>(null, exception.Message);
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
				var user = await usersRepository.GetUserByRefreshToken(refreshingToken.RefreshToken)
					?? throw new Exception("Пользователь с таким refresh_token не найден");

				var principal = tokenService.GetClaimsPrincipalFromExperedTokenWithValidation(user.AccessToken);

				refreshingToken = new JWTTokenDTO()
				{
					AccessToken = tokenService.GetAccessToken(principal.Claims.ToList(), out DateTime expires),
					RefreshToken = tokenService.GetRefreshToken()
				};
				await usersRepository.UpdateToken(user.Id, refreshingToken);
				return new ServiceResult<JWTTokenDTO>(refreshingToken, null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<JWTTokenDTO>(null, exception.Message);
			}
		}


		/// <summary>
		/// Метод для выхода пользователя из приложения, возвращает как результат вышедшего пользователя.
		/// </summary>
		public async Task<UserDTO> LogOut(JWTTokenDTO jwtTokenDTO) 
		{
			return await usersRepository.ClearToken(jwtTokenDTO);
		}
	}
}
