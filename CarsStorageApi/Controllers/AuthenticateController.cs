using CarsStorage.BLL.Abstractions;
using CarsStorage.BLL.Config;
using CarsStorageApi.AuthModels;
using CarsStorageApi.Config;
using CarsStorageApi.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CarsStorageApi.Controllers
{
	[ApiController]
	[AllowAnonymous]
	[Route("[controller]/[action]")]
		
	public class AuthenticateController(IAuthenticateService authService, IOptions<JwtDTOConfig> jwtDTOConfig, IOptions<RoleNamesConfig> roleNamesConfig) : ControllerBase
	{
		private readonly TokenMapper tokenMapper = new();
		private readonly JwtConfigMapper jwtConfigMapper = new();
		private readonly IOptions<RoleNamesConfig> roleNamesConfig = roleNamesConfig;

		[HttpPost]
		public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
		{
			if (string.IsNullOrWhiteSpace(registerDTO.UserName) 
				|| string.IsNullOrWhiteSpace(registerDTO.Email)
				|| string.IsNullOrWhiteSpace(registerDTO.Password))
				return BadRequest("Ошибка ввода данных пользователя");

			var userRoles = roleNamesConfig.Value.DefaultUserRoleNames;
			if (userRoles is null)
				return new BadRequestObjectResult("Ошибка конфигурации ролей пользователя");
			else 
				return await authService.Register(
					new RegisterAppUser()
					{
						UserName = registerDTO.UserName,
						Email = registerDTO.Email,
						Password = registerDTO.Password,
						Roles = userRoles
					});
		}

		[HttpPost]
		public async Task<ActionResult<TokenDTO>> LogIn([FromBody] LoginDTO loginDTO)
		{
			if (string.IsNullOrWhiteSpace(loginDTO.UserName) || string.IsNullOrWhiteSpace(loginDTO.Password))
				return BadRequest("Ошибка ввода данных пользователя");

			var tokenJWT = await authService.LogIn(
				loginDTO.UserName, loginDTO.Password, jwtConfigMapper.JwtDTOConfigToJwt(jwtDTOConfig.Value));
			if (tokenJWT.Value is not null) 
				return new OkObjectResult(tokenMapper.TokenJwtToTokenDto(tokenJWT.Value));
			return BadRequest("Ошибка аутентификации, не получен токен");
		}

		[Authorize]
		[HttpGet]
		public async Task LogOut()
		{
			await authService.LogOut();
		}
	}
}
