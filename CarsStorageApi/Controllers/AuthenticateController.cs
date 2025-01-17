
using CarsStorage.BLL.Abstractions;
using CarsStorage.BLL.Config;
using CarsStorageApi.Filters;
using CarsStorageApi.ModelsDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace CarsStorageApi.Controllers
{
	[ApiController]
	[AllowAnonymous]
	[Route("[controller]/[action]")]
	[ValidateModel]      //фильтр действия для возврата при Modelstate invalid - return BadRequest
	
	//https://learn.microsoft.com/ru-ru/aspnet/web-api/overview/formats-and-model-binding/model-validation-in-aspnet-web-api
	
	public class AuthenticateController(IAuthenticateService authService) : ControllerBase
	{
		private readonly IAuthenticateService authService = authService;
		private readonly TokenMapper tokenMapper = new();

		[HttpPost]
		public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
		{
			if (string.IsNullOrWhiteSpace(registerDTO.UserName) 
				|| string.IsNullOrWhiteSpace(registerDTO.Email)
				|| string.IsNullOrWhiteSpace(registerDTO.Password))
				return BadRequest();

			return await authService.Register(registerDTO.UserName, registerDTO.Email, registerDTO.Password);
		}

		[HttpPost]
		public async Task<ActionResult<string>> LogIn([FromBody] LoginDTO loginDTO)
		{
			if (string.IsNullOrWhiteSpace(loginDTO.UserName) || string.IsNullOrWhiteSpace(loginDTO.Password))
				return BadRequest();

			return await authService.LogIn(loginDTO.UserName, loginDTO.Password);
			//var token = await authService.LogIn(loginDTO.UserName, loginDTO.Password);
			//return tokenMapper.TokenJwtToTokenDto(token.Value);
		}

		[Authorize]
		[HttpGet]
		public async Task LogOut()
		{
			await authService.LogOut();
		}
	}
}
