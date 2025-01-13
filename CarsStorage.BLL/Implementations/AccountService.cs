using CarsStorage.BLL.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarsStorage.BLL.Implementations
{
	public class AccountService : IAccountService
	{
		private readonly SignInManager<AppUser> signInManager;
		private readonly UserManager<AppUser> userManager;
		public AccountService(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
		{
			this.signInManager = signInManager;
			this.userManager = userManager;
		}

		//https://learn.microsoft.com/ru-ru/aspnet/web-api/overview/formats-and-model-binding/model-validation-in-aspnet-web-api
		//может быть сделать потом фильтр валидации, чтобы фильтр возвращал HTTP-ответ, содержащий ошибки проверки
		public async Task<StatusCodeResult> LogIn(AppUser appUser)
		{
			var user = await userManager.FindByNameAsync(appUser.UserName);
			if (user is not null)
			{
				await signInManager.SignOutAsync();
				var result = await signInManager.PasswordSignInAsync(
					user, appUser.Password, false, false);
				if (result.Succeeded)
					return new StatusCodeResult(200);
				return new StatusCodeResult(400);   //Bad Request, например, пароль неверный
			}
			return new StatusCodeResult(401);		//не авторизован, т.к. не найден в БД	
		}

		public async Task LogOut()
		{	
			await signInManager.SignOutAsync();
		}
	}
}
