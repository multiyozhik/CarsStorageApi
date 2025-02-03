using AutoMapper;
using CarsStorage.BLL.Abstractions.Interfaces;
using CarsStorage.BLL.Abstractions.Models;
using CarsStorage.BLL.Repositories.Interfaces;
using CarsStorage.DAL.Entities;
using CarsStorage.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace CarsStorage.BLL.Implementations.Services
{
	/// <summary>
	/// Сервис для аутентификации.
	/// </summary>
	/// <param name="signInManager">Объект Identity - API для входа пользователей.</param>
	/// <param name="userManager">Объект Identity - API для управления пользователями.</param>
	/// <param name="tokenService">Объект сервиса для генерации и обновления токена.</param>
	/// <param name="roleRepository">Объект репозитория для ролей пользователя.</param>
	/// <param name="mapper">Объект меппера для сопоставления объектов.</param>
	/// <param name="initialOptions">Объект для настроек заполнения БД с названием роли при регистрации нового пользователя.</param>

	public class AuthenticateService(
		SignInManager<IdentityAppUser> signInManager, UserManager<IdentityAppUser> userManager, 
		ITokensService tokenService, IUsersRepository usersRepository, IRolesRepository roleRepository, 
		IMapper mapper, IOptions<InitialDbSeedConfig> initialOptions) : IAuthenticateService
	{
		/// <summary>
		/// Метод для регистрации пользователя в приложении.
		/// </summary>
		/// <param name="appUserRegisterDTO">Объект пользователя с UserName, Email, Password.</param>
		/// <returns></returns>
		public async Task<ServiceResult<AppUserDTO>> Register(AppUserRegisterDTO appUserRegisterDTO)
		{
			try
			{
				var user = new IdentityAppUser 
				{ 
					UserName = appUserRegisterDTO.UserName, 
					Email = appUserRegisterDTO.Email 
				};
				await userManager.CreateAsync(user, appUserRegisterDTO.Password);
				var defaultRolesNamesList = new List<string>() { initialOptions.Value.DefaultRoleName };
				user.RolesList = await roleRepository.GetRolesByNamesList(defaultRolesNamesList);
				return new ServiceResult<AppUserDTO>(mapper.Map<AppUserDTO>(user), null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<AppUserDTO>(null, exception.Message);
			}
		}


		/// <summary>
		/// Метод для входа пользователя в приложении.
		/// </summary>
		/// <param name="appUserLoginDTO">Объект пользователя с UserName и Password.</param>
		/// <returns></returns>
		public async Task<ServiceResult<JWTTokenDTO>> LogIn(AppUserLoginDTO appUserLoginDTO) 
		{
			try
			{				
				var user = await userManager.FindByNameAsync(appUserLoginDTO.UserName)
					?? throw new Exception("Ошибка - неверный логин.");
				var signinResult = await signInManager.PasswordSignInAsync(user, appUserLoginDTO.Password, false, false);
				if (!signinResult.Succeeded) 
					throw new Exception("Ошибка - неверный пароль.");

				var claimsList = roleRepository.GetClaimsByUser(mapper.Map<IdentityAppUser>(appUserLoginDTO));
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
				await usersRepository.UpdateToken(user.Id.ToString(), refreshingToken);
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
		public async Task<AppUserDTO> LogOut(JWTTokenDTO jwtTokenDTO) 
		{
			return await usersRepository.ClearToken(jwtTokenDTO);
		}
	}
}
