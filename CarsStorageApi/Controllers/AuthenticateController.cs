using AutoMapper;
using CarsStorage.Abstractions.BLL.Services;
using CarsStorage.Abstractions.ModelsDTO.Token;
using CarsStorage.Abstractions.ModelsDTO.User;
using CarsStorageApi.Config;
using CarsStorageApi.Filters;
using CarsStorageApi.Models.TokenModels;
using CarsStorageApi.Models.UserModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace CarsStorageApi.Controllers
{
	/// <summary>
	/// Класс контроллера для аутентификации пользователя.
	/// </summary>
	/// <param name="authService">Объект сервиса для аутентификации.</param>
	/// <param name="usersService">Объект сервиса для пользователей.</param>
	/// <param name="mapper">Объект меппера.</param>
	/// <param name="initialConfig">Объект начальной конфигурации.</param>
	/// <param name="logger">Объект для выполнения логирования.</param>
	[ApiController]
	[Route("[controller]")]
	[ServiceFilter(typeof(AcceptHeaderActionFilter))]
	public class AuthenticateController(IAuthenticateService authService, IUsersService usersService, IMapper mapper, IOptions<InitialConfig> initialConfig, ILogger<AuthenticateController> logger) : ControllerBase
	{
		/// <summary>
		/// Метод контроллера для регистрации пользователя в приложении.
		/// </summary>
		/// <param name="registerUserRequest">Объект данных пользователя для его регистрации.</param>
		/// <returns>Объект данных пользователя, возвращаемых клиенту.</returns>
		[AllowAnonymous]
		[HttpPost("Register")]
		[ProducesResponseType(typeof(List<UserResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<UserResponse>> Register([FromBody] RegisterUserRequest registerUserRequest)
		{
			try
			{
				var userCreaterDTO = mapper.Map<UserCreaterDTO>(registerUserRequest);
				userCreaterDTO.RoleNamesList = [initialConfig.Value.InitialRoleName];
				var serviceResult = await usersService.Create(userCreaterDTO);
				if (serviceResult.IsSuccess)
					return mapper.Map<UserResponse>(serviceResult.Result);
				throw serviceResult.ServiceError;
			}
			catch(Exception exception)
			{
				logger.LogError("Ошибка в {controller} в методе {method} при регистрации пользователя в приложении: {errorMessage}", this, nameof(this.Register), exception.Message);
				throw;
			}
		}


		/// <summary>
		/// Метод контроллера для входа пользователя в приложение.
		/// </summary>
		/// <param name="loginDataRequest">Объект данных пользователя для входа в приложение.</param>
		/// <returns>Объект токена доступа.</returns>
		[AllowAnonymous]
		[HttpPost("LogIn")]
		[ProducesResponseType(typeof(List<JWTTokenRequestResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<JWTTokenRequestResponse>> LogIn([FromBody] LoginUserRequest loginDataRequest)
		{
			try
			{
				var serviceResult = await authService.LogIn(mapper.Map<UserLoginDTO>(loginDataRequest));
				if (serviceResult.IsSuccess)
					return mapper.Map<JWTTokenRequestResponse>(serviceResult.Result);
				throw serviceResult.ServiceError;
			}
			catch(Exception exception)
			{
				logger.LogError("Ошибка в {controller} в методе {method} при входе пользователя в приложение: {errorMessage}", this, nameof(this.LogIn), exception.Message);
				throw;
			}
		}


		/// <summary>
		/// Метод-обработчик при успешной аутентификации в Google, перенаправляет на обработку пользовательских данных.
		/// </summary>
		[AllowAnonymous]               //по url https://localhost:{port}/Authenticate/signin-google
		[HttpGet("signin-google")]     //должен совпадать с redirect uri при регистрации в Google Api https://localhost:{port}/signin-google
		[ProducesResponseType(typeof(Task), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task GoogleLogin()
		{
			try 
			{
				await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties
				{
					RedirectUri = Url.Action("AuthResponseHandler", "Authenticate", new { authScheme = CookieAuthenticationDefaults.AuthenticationScheme })
				});
			}
			catch(Exception exception) 
			{
				logger.LogError("Ошибка в {controller} в методе {method} при входе пользователя в приложение при успешной аутентификации в Google: {errorMessage}", 
					this, nameof(GoogleLogin), exception.Message);
				throw;
			}
		}


		/// <summary>
		/// Метод-обработчик при успешной аутентификации в GitHub, перенаправляет на обработку пользовательских данных.
		/// </summary>
		[AllowAnonymous]               //по url https://localhost:{port}/Authenticate/signin-github
		[HttpGet("signin-github")]    //должен совпадать с redirect uri при регистрации в GitHub https://localhost:{port}/signin-github
		[ProducesResponseType(typeof(Task), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task GitHubLogin()
		{
			try
			{
				await HttpContext.ChallengeAsync("GitHub", new AuthenticationProperties
				{
					RedirectUri = Url.Action("AuthResponseHandler", "Authenticate", new { authScheme = "GitHub" })
				});
			}
			catch (Exception exception)
			{
				logger.LogError("Ошибка в {controller} в методе {method} при входе пользователя в приложение при успешной аутентификации в GitHub: {errorMessage}", 
					this, nameof(this.GitHubLogin), exception.Message);
				throw;
			}
		}


		/// <summary>
		/// Метод обработки ответа от стороннего провайдера аутентификации для получения токена доступа.
		/// </summary>
		/// <param name="authScheme">Схема аутентификации.</param>
		/// <returns>Объект токена доступа.</returns>
		[HttpGet("AuthResponseHandler")]
		[ProducesResponseType(typeof(JWTTokenRequestResponse), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<JWTTokenRequestResponse>> AuthResponseHandler([FromQuery] string authScheme)
		{
			try
			{
				var authResult = await HttpContext.AuthenticateAsync(authScheme);
				if (!authResult.Succeeded)
				{
					return BadRequest();
				}
				var authUserDataDTO = new AuthUserDataDTO
				{
					UserName = authResult.Principal.FindFirst(ClaimTypes.Name)?.Value,
					Email = authResult.Principal.FindFirst(ClaimTypes.Email)?.Value,
					RolesNamesList = [initialConfig.Value.InitialRoleName]
				};
				var createUserServiceResult = await authService.CreateAuthUserIfNotExist(authUserDataDTO);
				if (!createUserServiceResult.IsSuccess)
					throw createUserServiceResult.ServiceError;
				var loginAuthUserServiceResult = await authService.LogInAuthUser(createUserServiceResult.Result);
				if (!loginAuthUserServiceResult.IsSuccess)
					throw loginAuthUserServiceResult.ServiceError;
				return mapper.Map<JWTTokenRequestResponse>(loginAuthUserServiceResult.Result);
				
			}
			catch (Exception exception)
			{
				logger.LogError("Ошибка в {controller} в {method} при обработке ответа от стороннего провайдера аутентификации: {errorMessage}", this, nameof(this.AuthResponseHandler), exception.Message);
				throw;
			}
		}


		/// <summary>
		/// Метод контроллера для обновления токена при истечении срока токена доступа.
		/// </summary>
		/// <param name="jwtTokenRequestResponse">Объект токена.</param>
		/// <returns>Обновленный объект токена.</returns>
		[AllowAnonymous]
		[HttpPut("RefreshToken")]
		[ProducesResponseType(typeof(JWTTokenRequestResponse), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<JWTTokenRequestResponse>> RefreshToken([FromBody] JWTTokenRequestResponse jwtTokenRequestResponse)
		{
			try
			{
				if (jwtTokenRequestResponse is null || string.IsNullOrEmpty(jwtTokenRequestResponse.RefreshToken))
					return Unauthorized();
				var serviceResult = await authService.RefreshToken(mapper.Map<JWTTokenDTO>(jwtTokenRequestResponse));

				if (serviceResult.IsSuccess)
					return mapper.Map<JWTTokenRequestResponse>(serviceResult.Result);
				throw serviceResult.ServiceError;
			}
			catch(Exception exception)
			{
				logger.LogError("Ошибка в {controller} в {method} при обновлении токена при истечении срока токена доступа: {errorMessage}", this, nameof(this.RefreshToken), exception.Message);
				throw;
			}
		}


		/// <summary>
		/// Метод контроллера для выхода из приложения.
		/// </summary>
		/// <param name="accessToken">Строка токена доступа.</param>
		/// <returns>Идентификатор пользователя, вышедшего из приложения.</returns>
		[Authorize]
		[HttpGet("LogOut")]
		[ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<int>> LogOut([FromHeader] string accessToken)
		{
			try
			{
				if (string.IsNullOrEmpty(accessToken))
					return Unauthorized();
				var serviceResult = await authService.LogOut(accessToken);
				if (serviceResult.IsSuccess)
					return serviceResult.Result;
				throw serviceResult.ServiceError;
			}
			catch (Exception exception)
			{
				logger.LogError("Ошибка в {controller} в {method} при выходe из приложения: {errorMessage}", this, nameof(this.LogOut), exception.Message);
				throw;
			}
		}
	}
}
