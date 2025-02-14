using AutoMapper;
using CarsStorage.Abstractions.BLL.Services;
using CarsStorage.Abstractions.ModelsDTO.Token;
using CarsStorage.Abstractions.ModelsDTO.User;
using CarsStorage.BLL.Services.Config;
using CarsStorageApi.Models.TokenModels;
using CarsStorageApi.Models.UserModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using CarsStorage.Abstractions.General;
using Newtonsoft.Json;

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
		/// Метод для перенаправления на GitHub для аутентификации.
		/// </summary>
		[AllowAnonymous]
		[HttpGet]
		public IActionResult GitHubLogin()
		{
			var redirectUrl = Url.Action("GitHubResponse", "Authenticate");
			var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
			return Challenge(properties, "GitHub");
		}


		/// <summary>
		/// Метод обработки ответа от GitHub провайдера аутентификации с получением токена.
		/// </summary>
		[AllowAnonymous]
		[HttpGet]
		public async Task<ActionResult<JWTTokenRequestResponse>> GitHubResponse(/*[FromHeader] string accessToken*/)
		{
			var authResult = HttpContext.AuthenticateAsync("GitHub").Result;
			if (!authResult.Succeeded)
				return Unauthorized(authResult.Failure);

			var userName = authResult.Principal.FindFirst(ClaimTypes.Name)?.Value;
			var accessToken = authResult.Properties.GetTokenValue("access_token");

			var gitHubUser = await GetGitHubUserInfo(accessToken);

			var initialRoleNamesList = new List<string>([initialConfig.Value.InitialRoleName]);

			var serviceResult = await authService.LogInAuthUser(gitHubUser, initialRoleNamesList);
			if (serviceResult.IsSuccess)
				return mapper.Map<JWTTokenRequestResponse>(serviceResult.Result);
			throw serviceResult.ServiceError;
		}


		/// <summary>
		/// Закрытые метод для получения информации о пользователе из GitHub сервиса аутентификации.
		/// </summary>
		private static async Task<GitHubUserDTO> GetGitHubUserInfo(string accessToken)
		{
			using var httpClient = new HttpClient();
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
			httpClient.DefaultRequestHeaders.Add("User-Agent", "CarsStoraheApi");
			var response = await httpClient.GetStringAsync($"https://api.github.com/user");
			return JsonConvert.DeserializeObject<GitHubUserDTO>(response);		
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
