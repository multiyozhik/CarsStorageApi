
using CarsStorage.BLL.Abstractions;
using CarsStorageApi.Filters;
using CarsStorageApi.ModelsDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CarsStorageApi.Controllers
{
	[ApiController]
	[AllowAnonymous]
	[Route("[controller]/[action]")]
	[ValidateModel]      //фильтр действия для возврата при Modelstate invalid - return BadRequest
	
	//https://learn.microsoft.com/ru-ru/aspnet/web-api/overview/formats-and-model-binding/model-validation-in-aspnet-web-api
	
	public class AuthenticateController(IAuthenticateService accountService) : ControllerBase
	{
		private readonly IAuthenticateService accountService = accountService;


		[HttpPost]
		public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
		{
			if (string.IsNullOrWhiteSpace(registerDTO.UserName) 
				|| string.IsNullOrWhiteSpace(registerDTO.Email)
				|| string.IsNullOrWhiteSpace(registerDTO.Password))
				return BadRequest();

			return await accountService.Register(registerDTO.UserName, registerDTO.Email, registerDTO.Password);			
		}

		[HttpPost]
		public async Task<IActionResult> LogIn([FromBody] LoginDTO loginDTO)
		{
			if (string.IsNullOrWhiteSpace(loginDTO.UserName) || string.IsNullOrWhiteSpace(loginDTO.Password))
				return BadRequest();

			return await accountService.LogIn(loginDTO.UserName, loginDTO.Password);
		}

		[Authorize]
		[HttpGet]
		public async Task LogOut()
		{
			await accountService.LogOut();
		}
	}
}
