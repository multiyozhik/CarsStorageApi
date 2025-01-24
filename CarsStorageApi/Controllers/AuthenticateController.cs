using AutoMapper;
using CarsStorage.BLL.Abstractions.Interfaces;
using CarsStorage.BLL.Abstractions.Models;
using CarsStorage.BLL.Implementations.Services;
using CarsStorageApi.AuthModels;
using CarsStorageApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarsStorageApi.Controllers
{
	[ApiController]
	[AllowAnonymous]
	[Route("[controller]/[action]")]
		
	public class AuthenticateController(IAuthenticateService authService, IMapper mapper) : ControllerBase
	{
		[HttpPost]
		public async Task<ActionResult<UserRequestResponse>> Register([FromBody] RegisterUserDataRequest registerUserDataRequest)
		{
			var serviceResult = await authService.Register(mapper.Map<AppUserRegisterDTO>(registerUserDataRequest));

			if (serviceResult.IsSuccess && serviceResult.Result is not null)
				return mapper.Map<UserRequestResponse>(serviceResult.Result);
			else
				return BadRequest(serviceResult.ErrorMessage);
		}

		[HttpPost]
		public async Task<ActionResult<JWTTokenResponse>> LogIn([FromBody] LoginDataRequest loginDataRequest)
		{
			var serviceResult = await authService.LogIn(mapper.Map<AppUserLoginDTO>(loginDataRequest));

			if (serviceResult.IsSuccess && serviceResult.Result is not null)
				return mapper.Map<JWTTokenResponse>(serviceResult.Result);
			else
				return BadRequest(serviceResult.ErrorMessage);
		}

		[Authorize]
		[HttpGet]
		public async Task LogOut()
		{
			await authService.LogOut();
		}

		private ActionResult<T> ReturnActionResult<T>(ServiceResult<T> serviceResult)
		{
			if (serviceResult.IsSuccess && serviceResult.Result is not null)
				return serviceResult.Result;
			else
				return BadRequest(serviceResult.ErrorMessage);
		}
	}
}
