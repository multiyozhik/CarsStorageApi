using CarsStorage.BLL.Repositories.Interfaces;
using CarsStorage.DAL.EF;
using CarsStorage.DAL.Entities;
using CarsStorage.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CarsStorage.BLL.Repositories.Implementations
{
	public class UsersRepository(IdentityAppDbContext dbContext, IServiceProvider serviceProvider) : IUsersRepository
	{
		private readonly UserManager<IdentityAppUser> userManager = serviceProvider.GetRequiredService<UserManager<IdentityAppUser>>();


		public async Task<IEnumerable<IdentityAppUser>> GetList()
		{
			return await dbContext.Users.ToListAsync();
		}


		public async Task<IdentityAppUser> GetById(int id)
		{
			return await userManager.FindByIdAsync(id.ToString());
		}


		public async Task<IdentityAppUser> Create(IdentityAppUserCreater identityAppUserCreater)
		{
			var rolesList = identityAppUserCreater.Roles.Select(roleName => new RoleEntity(roleName));

			var user = new IdentityAppUser
			{
				UserName = identityAppUserCreater.UserName,
				Email = identityAppUserCreater.Email,
				RolesList = rolesList.ToList()
			};

			await userManager.CreateAsync(user, identityAppUserCreater.Password);
			return user;
		}


		public async Task<IdentityAppUser> Update(IdentityAppUser identityAppUser)
		{
			var user = await userManager.FindByIdAsync(identityAppUser.Id.ToString())
				?? throw new Exception("Не найден пользователь по id");	

			user.UserName = identityAppUser.UserName;
			user.Email = identityAppUser.Email;
			user.RolesList = identityAppUser.RolesList;
			await userManager.UpdateAsync(user);
			return user;
		}

		public async Task Delete(int id)
		{
			var user = await userManager.FindByIdAsync(id.ToString()) 
				?? throw new Exception("Не найден пользователь по id");

			await userManager.DeleteAsync(user);
			
			//var identityAppUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id.ToString());
			//if (identityAppUser is not null)
			//{
			//	dbContext.Users.Remove(identityAppUser);
			//	await dbContext.SaveChangesAsync();
			//}
			//else
			//	throw new Exception("Пользователь с заданным Id не найден");
		}

	}
}
