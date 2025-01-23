using AutoMapper;
using CarsStorage.BLL.Abstractions.Interfaces;
using CarsStorage.BLL.Abstractions.Models;
using CarsStorageApi.AuthModels;
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
		public async Task<IActionResult> Register([FromBody] RegisterUserDataRequest registerUserDataRequest)
		{			
			return await authService.Register(mapper.Map<AppUserRegisterDTO>(registerUserDataRequest));
		}

		[HttpPost]
		public async Task<ActionResult<JWTTokenResponse>> LogIn([FromBody] LoginDataRequest loginDataRequest)
		{			
			var jwtTokenDTO = await authService.LogIn(mapper.Map<AppUserLoginDTO>(loginDataRequest));
			return mapper.Map<JWTTokenResponse>(jwtTokenDTO);
		}

		[Authorize]
		[HttpGet]
		public async Task LogOut()
		{
			await authService.LogOut();
		}
	}
}
