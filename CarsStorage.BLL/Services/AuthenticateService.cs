using AutoMapper;
using CarsStorage.BLL.Abstractions.Interfaces;
using CarsStorage.BLL.Abstractions.Models;
using CarsStorage.BLL.Abstractions.ModelsDTO.AuthModels;
using CarsStorage.BLL.Abstractions.ModelsDTO.UserDTO;
using CarsStorage.BLL.Repositories.Implementations;
using CarsStorage.BLL.Repositories.Interfaces;
using CarsStorage.DAL.Config;
using CarsStorage.DAL.Entities;
using CarsStorage.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace CarsStorage.BLL.Implementations.Services
{
    /// <summary>
    /// Сервис для аутентификации.
    /// </summary>
    /// <param name="tokenService">Объект сервиса для генерации и обновления токена.</param>
    /// <param name="roleRepository">Объект репозитория для ролей пользователя.</param>
    /// <param name="mapper">Объект меппера для сопоставления объектов.</param>
    /// <param name="initialOptions">Объект для настроек заполнения БД с названием роли при регистрации нового пользователя.</param>

    public class AuthenticateService(IUsersRepository usersRepository, IRolesRepository rolesRepository, ITokensService tokenService, 
		IMapper mapper, IOptions<InitialDbSeedConfig> initialOptions) : IAuthenticateService
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
					?? throw new Exception("Ошибка - неверный логин.");


				//var user = await userManager.FindByNameAsync(userLoginDTO.UserName)
				//	?? throw new Exception("Ошибка - неверный логин.");
				//var signinResult = await signInManager.PasswordSignInAsync(user, userLoginDTO.Password, false, false);
				//if (!signinResult.Succeeded)
				//	throw new Exception("Ошибка - неверный пароль.");

				var claimsList = roleRepository.GetClaimsByUser(mapper.Map<UserEntity>(userLoginDTO));
				var jwtTokenDTO = new JWTTokenDTO()
				{
					AccessToken = tokenService.GetAccessToken(claimsList, out DateTime accessTokenExpires),
					RefreshToken = tokenService.GetRefreshToken()
				};

				await usersRepository.UpdateToken(user.Id, jwtTokenDTO);
				return new ServiceResult<JWTTokenDTO>(jwtTokenDTO, null);				
			}
			catch (Exception exception)
			{
				return new ServiceResult<JWTTokenDTO>(null, exception.Message);
			}
		}


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
		/// Метод для выхода пользователя из приложения.
		/// </summary>
		/// <returns></returns>
		public async Task<UserDTO> LogOut(JWTTokenDTO jwtTokenDTO) 
		{
			return await usersRepository.ClearToken(jwtTokenDTO);
		}
	}
}
