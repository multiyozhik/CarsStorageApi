using AutoMapper;
using CarsStorage.Abstractions.BLL.Services;
using CarsStorage.Abstractions.General;
using CarsStorage.Abstractions.ModelsDTO.Token;
using CarsStorage.Abstractions.ModelsDTO.User;
using CarsStorageApi.Config;
using CarsStorageApi.Models.TokenModels;
using CarsStorageApi.Models.UserModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace CarsStorageApi.Controllers
{
	/// <summary>
	/// Класс контроллер для аутентификации пользователя.
	/// </summary>
	[ApiController]
	[Route("[controller]/[action]")]
	public class AuthenticateController(IAuthenticateService authService, IUsersService usersService, IMapper mapper, IOptions<InitialConfig> initialConfig, IOptions<GitHubConfig> gitHubConfig) : ControllerBase
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
		/// Метод для аутентификации в GitHub.
		/// </summary>
		[AllowAnonymous]
		[HttpGet]
		//[Authorize(AuthenticationSchemes = "GitHub")]
		//public async Task<HttpResponseMessage> GitHubLogin()
		public IActionResult GitHubLogin()
		{
			string state = RandomGenerator.GenerateState();
			
			//var authorizeUrl = $"https://github.com/login/oauth/authorize?client_id={gitHubConfig.Value.ClientId}&redirect_uri={gitHubConfig.Value.RedirectUri}&scope=user&state={state}";
			//var authorizeUrl = $"https://github.com/login/oauth/authorize?client_id={gitHubConfig.Value.ClientId}&redirect_uri=http://127.0.0.1:7251/Authenticate/GitHubResponse&scope=user&state={state}";
			////var authorizeUrl = $"https://github.com/login/oauth/authorize?client_id={gitHubConfig.Value.ClientId}&scope=user";
			//return Redirect(authorizeUrl);

			//https://github.com/login/oauth/authorize?client_id=Ov23liCK3lqpBsOCWGtz&redirect_uri=https://localhost:7251/Authenticate/GitHubResponse

			//https://github.com/login/oauth/authorize?client_id=Ov23liCK3lqpBsOCWGtz&redirect_uri=/Authenticate/GitHubResponse&scope=username,email
			//https://github.com/login/oauth/authorize?client_id=Ov23liCK3lqpBsOCWGtz&redirect_uri=/Authenticate/GitHubResponse&scope=user:email&state=12345

			var properties = new AuthenticationProperties	{RedirectUri = Url.Action("GitHubResponse", "Authenticate")	};
			return Challenge(properties, "GitHub");
		}


		/// <summary>
		/// Метод обработки ответа от GitHub провайдера аутентификации с получением токена доступа.
		/// </summary>
		//[Authorize]
		[AllowAnonymous]
		[HttpGet]
		public async Task<ActionResult<JWTTokenRequestResponse>> GitHubResponse()
		{
			var authResult = HttpContext.AuthenticateAsync("GitHub").Result;
			if (!authResult.Succeeded)
				return Unauthorized(authResult.Failure);
			var authUserData = new AuthUserData
			{
				UserName = authResult.Principal.FindFirst(ClaimTypes.Name)?.Value,
				Email = authResult.Principal.FindFirst(ClaimTypes.Email)?.Value,
				AccessTokenFromAuthService = authResult.Properties.GetTokenValue("access_token"),
				RolesNamesList = [initialConfig.Value.InitialRoleName]
			};
			var serviceResult = await authService.LogInAuthUser(authUserData);
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
