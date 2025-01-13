
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarsStorageApi.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class AccountController: ControllerBase
	{
		private readonly SignInManager<LoginDTO> signInManager;
		private readonly Microsoft.AspNetCore.Identity.UserManager<LoginDTO> userManager;
		public AccountController(SignInManager<LoginDTO> signInManager, Microsoft.AspNetCore.Identity.UserManager<LoginDTO> userManager)
		{
			this.signInManager = signInManager;
			this.userManager = userManager;
		}

		[AllowAnonymous]
		[HttpPost]
		public async Task<StatusCodeResult> LogIn([FromBody] LoginDTO loginDTO)
		{
			if (ModelState.IsValid)
			{
				var user = await userManager.FindByNameAsync(loginDTO.UserName);
				if (user is not null)
				{
					await signInManager.SignOutAsync();
					var result = await signInManager.PasswordSignInAsync(
						user, loginDTO.Password, false, false);
					if (result.Succeeded)
						return new StatusCodeResult(200);
					return new StatusCodeResult(400);
				}
				return new StatusCodeResult(401);
			}
			return new StatusCodeResult(400);
		}

		[Authorize]
		[HttpGet, ValidateAntiForgeryToken]
		public async Task LogOut()
		{
			await signInManager.SignOutAsync();
		}
	}
}
