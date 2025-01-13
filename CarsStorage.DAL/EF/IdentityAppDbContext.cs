using CarsStorage.DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarsStorage.DAL.EF
{
	public class IdentityAppDbContext : IdentityDbContext<IdentityAppUser>
	{
		//public DbSet<IdentityAppUser>? IdentityAppUsers { get; }
		public IdentityAppDbContext(DbContextOptions<IdentityAppDbContext> options) : base(options)
		{
		}

		public static async Task CreateAdminAccount(
			IServiceProvider serviceProvider, IConfiguration config)
		{
			UserManager<IdentityAppUser> userManager =
				serviceProvider.GetRequiredService<UserManager<IdentityAppUser>>();
			RoleManager<IdentityRole> roleManager =
				serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

			string username = config["AdminUser:UserName"];
			string password = config["AdminUser:Password"];
			string role = config["AdminUser:Role"];

			if (await userManager.FindByNameAsync(username) == null)
			{
				if (await roleManager.FindByNameAsync(role) == null)
				{ 
					await roleManager.CreateAsync(new IdentityRole(role)); 
				}
				var user = new IdentityAppUser { UserName = username };
				IdentityResult result = await userManager.CreateAsync(user, password);
				if (result.Succeeded) 
				{
					await userManager.AddToRoleAsync(user, role);
				}
			}
		}
	}
}
