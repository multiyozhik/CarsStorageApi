using CarsStorage.BLL.Abstractions;
using CarsStorage.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.Extensions.Configuration;
using System.Web.Http.ModelBinding;

namespace CarsStorage.BLL.Implementations
{
	public class AuthenticateService(SignInManager<IdentityAppUser> signInManager, UserManager<IdentityAppUser> userManager, IConfiguration config) : IAuthenticateService
	{
		private readonly SignInManager<IdentityAppUser> signInManager = signInManager;
		private readonly UserManager<IdentityAppUser> userManager = userManager;
		private readonly IConfiguration config = config;

		public async Task<IActionResult> Register(string userName, string email, string password)
		{
			var user = new IdentityAppUser { UserName = userName, Email = email };
			var result = await userManager.CreateAsync(user, password);
			if (result.Succeeded)
			{
				await userManager.AddToRoleAsync(user, config["RoleNames:AuthUserRoleName"]);
				await signInManager.SignInAsync(user, isPersistent: false); 
				return new StatusCodeResult(200);
			}
			return new StatusCodeResult(401);
		}

		public async Task<IActionResult> LogIn(string userName, string password)
		{
			var user = await userManager.FindByNameAsync(userName);
			if (user is not null)
			{
				await signInManager.SignOutAsync();
				var result = await signInManager.PasswordSignInAsync(
					user, password, false, false);
				if (result.Succeeded)
					return new StatusCodeResult(200);
				return new StatusCodeResult(400);   //Bad Request, например, пароль неверный
			}
			return new StatusCodeResult(401);		//не авторизован, т.к. не найден в БД	
		}

		public async Task LogOut()      //возможно httpcontext передавать?
		{	
			await signInManager.SignOutAsync();
		}
	}
}
