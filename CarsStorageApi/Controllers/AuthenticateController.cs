using AutoMapper;
using CarsStorage.BLL.Abstractions.Interfaces;
using CarsStorage.BLL.Abstractions.Models;
using CarsStorageApi.AuthModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarsStorageApi.Controllers
{
	/// <summary>
	/// Класс контроллер для аутентификации пользователя.
	/// </summary>
	/// <param name="authService"></param>
	/// <param name="mapper"></param>
	[ApiController]
	[Route("[controller]/[action]")]		
	public class AuthenticateController(IAuthenticateService authService, IMapper mapper) : ControllerBase
	{
		/// <summary>
		/// Метод контроллера регистрации пользователя.
		/// </summary>
		/// <param name="registerUserDataRequest">Объект типа <see cref="RegisterUserDataRequest"/> с данными пользователя, передаваемыми для регистрации.</param>
		/// <returns>Асинхронная задача, представляющая объект типа <see cref="UserRequestResponse" - зарегистрированного пользователя.</returns>
		[AllowAnonymous]
		[HttpPost]
		public async Task<ActionResult<UserRequestResponse>> Register([FromBody] RegisterUserDataRequest registerUserDataRequest)
		{
			var serviceResult = await authService.Register(mapper.Map<AppUserRegisterDTO>(registerUserDataRequest));
			if (serviceResult.IsSuccess && serviceResult.Result is not null)
				return mapper.Map<UserRequestResponse>(serviceResult.Result);
			else
				return BadRequest(serviceResult.ErrorMessage);
		}


		/// <summary>
		/// Метод контроллера для входа пользователя в приложение.
		/// </summary>
		/// <param name="loginDataRequest">Объект типа <see cref="LoginDataRequest"/> с данными пользователя для входа в приложение.</param>
		/// <returns>Асинхронная задача, представляющая объект типа <see cref="JWTTokenRequestResponse" - токен для доступа в приложение.</returns>
		
		[AllowAnonymous]
		[HttpPost]
		public async Task<ActionResult<JWTTokenRequestResponse>> LogIn([FromBody] LoginDataRequest loginDataRequest)
		{
			var serviceResult = await authService.LogIn(mapper.Map<AppUserLoginDTO>(loginDataRequest));
			if (serviceResult.IsSuccess && serviceResult.Result is not null)
				return mapper.Map<JWTTokenRequestResponse>(serviceResult.Result);
			else
				return BadRequest(serviceResult.ErrorMessage);
		}

		/// <summary>
		/// Метод контроллера для обновления токена доступа.
		/// </summary>
		/// <param name="jwtTokenRequestResponse">Объект типа <see cref="JWTTokenRequestResponse"/> - обновляемый токен.</param>
		/// <returns>Асинхронная задача, представляющая объект типа <see cref="JWTTokenRequestResponse" - токен для доступа в приложение.</returns>
		[AllowAnonymous]
		[HttpPost]
		public async Task<ActionResult<JWTTokenRequestResponse>> RefreshToken([FromBody] JWTTokenRequestResponse jwtTokenRequestResponse)
		{
			if (jwtTokenRequestResponse is null || string.IsNullOrEmpty(jwtTokenRequestResponse.RefreshToken))
				return Unauthorized();
			var serviceResult = await authService.RefreshToken(mapper.Map<JWTTokenDTO>(jwtTokenRequestResponse));

			if (serviceResult.IsSuccess && serviceResult.Result is not null)
				return mapper.Map<JWTTokenRequestResponse>(serviceResult.Result);
			else
				return BadRequest(serviceResult.ErrorMessage);
		}

		/// <summary>
		/// Метод контроллера для выхода из приложения.
		/// </summary>
		/// <returns>Асинхронная задача - выход из приложения.</returns>
		[Authorize]
		[HttpGet]
		public async Task<ActionResult<UserRequestResponse>> LogOut([FromBody] JWTTokenRequestResponse jwtTokenRequestResponse)
		{
			if (jwtTokenRequestResponse is null || string.IsNullOrEmpty(jwtTokenRequestResponse.RefreshToken))
				return Unauthorized();
			var appUserDto = await authService.LogOut(mapper.Map<JWTTokenDTO>(jwtTokenRequestResponse));
			return mapper.Map<UserRequestResponse>(appUserDto);
		}
	}
}
