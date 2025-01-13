
using CarsStorage.BLL.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarsStorageApi.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class AccountController: ControllerBase
	{
		private readonly IAccountService accountService;
		public AccountController(IAccountService accountService)
		{
			this.accountService = accountService;
		}

		[AllowAnonymous]
		[HttpPost]
		public async Task<StatusCodeResult> LogIn([FromBody] LoginDTO loginDTO)
		{
			return await accountService.LogIn(new AppUser() { UserName = loginDTO.UserName });
		}

		[Authorize]
		[HttpGet, ValidateAntiForgeryToken]
		public async Task LogOut()
		{
			await accountService.LogOut();
		}
	}
}
