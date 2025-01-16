using CarsStorage.BLL.Abstractions;
using CarsStorage.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarsStorage.BLL.Implementations
{
	public class UsersService(
		UserManager<IdentityAppUser> usrMgr) : IUsersService
	{
		private readonly UserManager<IdentityAppUser> userManager = usrMgr;

		public async Task<IEnumerable<AppUser>> GetList()
		{
			var users = userManager.Users.ToList();
			var appUsers = users.Select(u => new AppUser
			{
				Id = new Guid(u.Id),
				UserName = u.UserName,
				Email = u.Email,
				Roles = userManager.GetRolesAsync(u).Result
			});
			return appUsers;
		}

		public async Task<ActionResult<AppUser>> GetById(Guid id)
		{
			var user = await userManager.FindByIdAsync(id.ToString());

			if (user is null) return new NotFoundResult();

			var roles = await userManager.GetRolesAsync(user);

			return new AppUser { Id = new Guid(user.Id), UserName = user.UserName, Email = user.Email , Roles = roles};
		}


		public async Task<IActionResult> Create(RegisterAppUser registerAppUser)
		{
			var user = new IdentityAppUser { UserName = registerAppUser.UserName, Email = registerAppUser.Email };
			if (registerAppUser.Password is null) 
			{ 
				return new BadRequestResult(); 
			};
			var result = await userManager.CreateAsync(user, registerAppUser.Password);
			if (result.Succeeded) {
				return new StatusCodeResult(200); 
			}
			return new StatusCodeResult(500);
		}

		public async Task<IActionResult> Update(AppUser appUser)
		{
			var user = await userManager.FindByIdAsync(appUser.Id.ToString());
            if (user is null)
            {
				return new BadRequestResult();
            }
            user.UserName = appUser.UserName;
			user.Email = appUser.Email;
			var roles = await userManager.GetRolesAsync(user);
			var result = await userManager.RemoveFromRolesAsync(user, roles);
			if (result.Succeeded)
			{
				result = await userManager.AddToRolesAsync(user, roles);
				if (result.Succeeded)
				{
					return new StatusCodeResult(200);
				}
			}
			return new StatusCodeResult(500);
		}

		public async Task<IActionResult> Delete(Guid id)
		{
			var user = await userManager.FindByIdAsync(id.ToString());
			var result = await userManager.DeleteAsync(user);
			if (result.Succeeded)
			{
				return new StatusCodeResult(200);
			}
			return new StatusCodeResult(500);
		}
	}
}
