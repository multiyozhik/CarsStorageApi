using CarsStorage.BLL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsStorage.BLL.Repositories.Implementations
{
	public class UsersRepository : IUsersRepository
	{
		public async Task<IEnumerable<AppUserDTO>> GetList()
		{
			//ToDo: не пойму, как сделать без ToList? чтоб не выгружать, а всю работу на стороне сервера
			//var users = userManager.Users;       //через IQyerable
			var users = userManager.Users.ToList();

			var tasks = users.Select(
				async (u) =>
				{
					var roles = await userManager.GetRolesAsync(u);
					return new AppUserDTO()
					{
						Id = new Guid(u.Id),
						UserName = u.UserName,
						Email = u.Email,
						Roles = roles
					};
				}).ToList();
			return await Task.WhenAll(tasks);
		}


		public async Task<ActionResult<AppUserDTO>> GetById(Guid id)
		{
			var user = await userManager.FindByIdAsync(id.ToString());

			if (user is null)
				return new NotFoundObjectResult("Не найден пользователь по id");

			var roles = await userManager.GetRolesAsync(user);

			return new AppUserDTO { Id = new Guid(user.Id), UserName = user.UserName, Email = user.Email, Roles = roles };
		}


		public async Task<IActionResult> Create(AppUserRegixteDTO registerAppUser)
		{
			var user = new IdentityAppUser
			{
				UserName = registerAppUser.UserName,
				Email = registerAppUser.Email
			};

			if (string.IsNullOrEmpty(registerAppUser.Password))
				return new BadRequestObjectResult("Не задан пароль для пользователя");
			await userManager.CreateAsync(user, registerAppUser.Password);

			if (registerAppUser.Roles is null)
				return new BadRequestObjectResult("Не заданы роли для пользователя");

			foreach (var role in registerAppUser.Roles)
			{
				await userManager.AddToRoleAsync(user, role);
			}
			return new OkResult();
		}


		public async Task<IActionResult> Update(AppUserDTO appUser)
		{
			var user = await userManager.FindByIdAsync(appUser.Id.ToString());
			if (user is null)
				return new NotFoundObjectResult("Не найден пользователь по id");

			if (appUser.Roles is null)
				return new BadRequestObjectResult("Не заданы роли для пользователя");

			user.UserName = appUser.UserName;
			user.Email = appUser.Email;
			var roles = await userManager.GetRolesAsync(user);
			var result = await userManager.RemoveFromRolesAsync(user, roles);
			if (result.Succeeded)
			{
				result = await userManager.AddToRolesAsync(user, appUser.Roles);
				if (result.Succeeded)
					return new OkResult();
			}
			return new StatusCodeResult(500);
		}

		public async Task<IActionResult> Delete(Guid id)
		{
			var user = await userManager.FindByIdAsync(id.ToString());
			if (user is null)
				return new NotFoundObjectResult("Не найден пользователь по id");

			var result = await userManager.DeleteAsync(user);
			if (result.Succeeded)
				if (result.Succeeded)
					return new OkResult();
			return new StatusCodeResult(500);
		}


	}
}
