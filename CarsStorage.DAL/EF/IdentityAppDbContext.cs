using CarsStorage.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;

namespace CarsStorage.DAL.EF
{
	/// <summary>
	///  DbContext для таблицы пользователей в Identity. 
	/// </summary>
	/// <param name="options"></param>
	public class IdentityAppDbContext(DbContextOptions<IdentityAppDbContext> options) : IdentityDbContext<IdentityAppUser>(options)
	{
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			var passwordHasher = new PasswordHasher<IdentityAppUser>();

			var identityAppUserList = new List<IdentityAppUser>();

			for (int i = 0; i < 10; i++)
			{
				identityAppUserList[i] = new IdentityAppUser
				{
					UserName = $"user{i}",
					Email = $"user{i}@mail.ru"
				};

				identityAppUserList[i].PasswordHash = passwordHasher.HashPassword(identityAppUserList[i], "user1");
			}

			modelBuilder.Entity<IdentityAppUser>().HasData(identityAppUserList);
		}
	}
}
