using AutoMapper;
using CarsStorage.BLL.Abstractions.Services;
using CarsStorage.BLL.Abstractions.ModelsDTO.Token;
using CarsStorage.BLL.Abstractions.ModelsDTO.User;
using CarsStorageApi.Models.TokenModels;
using CarsStorageApi.Models.UserModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using CarsStorage.BLL.Implementations.Config;

namespace CarsStorageApi.Controllers
{
	/// <summary>
	/// Класс контроллер для аутентификации пользователя.
	/// </summary>
	[ApiController]
	[Route("[controller]/[action]")]		
	public class AuthenticateController(IAuthenticateService authService, IUsersService usersService, IMapper mapper, IOptions<InitialConfig> initialConfig) : ControllerBase
	{
		/// <summary>
		/// Метод контроллера регистрации пользователя (для нового пользователя устанавливается начальный спсиок ролей из начальной конфигурации приложения).
		/// </summary>
		[AllowAnonymous]
		[HttpPost]
		public async Task<ActionResult<UserResponse>> Register([FromBody] RegisterUserRequest registerUserRequest)
		{

			var userCreaterDTO = mapper.Map<UserCreaterDTO>(registerUserRequest);
			userCreaterDTO.RoleNamesList = [initialConfig.Value.InitialRoleName];
			var serviceResult = await usersService.Create(userCreaterDTO);
			if (serviceResult.IsSuccess)
				return mapper.Map<UserResponse>(serviceResult.Result);
			throw serviceResult.ServiceError;
		}

		/// <summary>
		/// Метод контроллера для входа пользователя в приложение возвращает токен.
		/// </summary>
		[AllowAnonymous]
		[HttpPost]
		public async Task<ActionResult<JWTTokenRequestResponse>> LogIn([FromBody] LoginUserRequest loginDataRequest)
		{
			var serviceResult = await authService.LogIn(mapper.Map<UserLoginDTO>(loginDataRequest));
			if (serviceResult.IsSuccess)
				return mapper.Map<JWTTokenRequestResponse>(serviceResult.Result);
			throw serviceResult.ServiceError;
		}


		/// <summary>
		/// Метод контроллера для обновления токена при истечении срока токена доступа.
		/// </summary>
		[AllowAnonymous]
		[HttpPut]
		public async Task<ActionResult<JWTTokenRequestResponse>> RefreshToken([FromBody] JWTTokenRequestResponse jwtTokenRequestResponse)
		{
			if (jwtTokenRequestResponse is null || string.IsNullOrEmpty(jwtTokenRequestResponse.RefreshToken))
				return Unauthorized();
			var serviceResult = await authService.RefreshToken(mapper.Map<JWTTokenDTO>(jwtTokenRequestResponse));

			if (serviceResult.IsSuccess)
				return mapper.Map<JWTTokenRequestResponse>(serviceResult.Result);
			throw serviceResult.ServiceError;
		}


		/// <summary>
		/// Метод контроллера для выхода из приложения (в заголовке передаем токен доступа).
		/// </summary>
		[Authorize]
		[HttpGet]
		public async Task<ActionResult<int>> LogOut([FromHeader] string accessToken)
		{
			if (string.IsNullOrEmpty(accessToken))
				return Unauthorized();
			var serviceResult = await authService.LogOut(accessToken);
			if (serviceResult.IsSuccess)				
				return serviceResult.Result;
			throw serviceResult.ServiceError;
		}
	}
}
