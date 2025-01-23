using CarsStorage.BLL.Abstractions.Interfaces;
using CarsStorage.BLL.Abstractions.Models;
using CarsStorageApi.AuthModels;
using CarsStorageApi.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarsStorageApi.Controllers
{
	[ApiController]
	[AllowAnonymous]
	[Route("[controller]/[action]")]
		
	public class AuthenticateController(IAuthenticateService authService) : ControllerBase
	{
		private readonly JWTTokenMapper tokenMapper = new();
		private readonly UserMapper userMapper = new();

		[HttpPost]
		public async Task<IActionResult> Register([FromBody] RegisterUserDataRequest registerUserDataRequest)
		{
			return await authService.Register(userMapper.RegisterUserDataRequestToAppUserRegisterDTO(registerUserDataRequest));
		}

		[HttpPost]
		public async Task<ActionResult<JWTTokenResponse>> LogIn([FromBody] LoginDataRequest loginDataRequest)
		{
			var JWTtokenDTO = await authService.LogIn(userMapper.LoginDataRequestToAppUserLoginDTO(loginDataRequest));
			return new OkObjectResult(tokenMapper.JwtTokenDtoToJwtTokenResponse(JWTtokenDTO.Value));
		}

		[Authorize]
		[HttpGet]
		public async Task LogOut()
		{
			await authService.LogOut();
		}
	}
}
