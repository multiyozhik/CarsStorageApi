using CarsStorage.BLL.Abstractions;
using CarsStorage.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarsStorage.BLL.Implementations
{
	public class AccountService(SignInManager<IdentityAppUser> signInManager, UserManager<IdentityAppUser> userManager) : IAccountService
	{
		private readonly SignInManager<IdentityAppUser> signInManager = signInManager;
		private readonly UserManager<IdentityAppUser> userManager = userManager;

		//https://learn.microsoft.com/ru-ru/aspnet/web-api/overview/formats-and-model-binding/model-validation-in-aspnet-web-api
		//TODO: может быть сделать потом фильтр валидации, чтобы фильтр возвращал HTTP-ответ, содержащий ошибки проверки
		public async Task<StatusCodeResult> LogIn(AppUser appUser)
		{
			//TODO: здесь нужна какая-то проверка валидации на сервере типа ModelState
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

		public async Task LogOut()      //возможно httpcontext передавать?
		{	
			await signInManager.SignOutAsync();
		}
	}
}
