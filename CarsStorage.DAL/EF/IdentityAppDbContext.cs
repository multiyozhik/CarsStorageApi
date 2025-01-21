using CarsStorage.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarsStorage.DAL.EF
{
	public class IdentityAppDbContext(DbContextOptions<IdentityAppDbContext> options) : IdentityDbContext<IdentityAppUser>(options)
	{
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<IdentityAppUser>().HasData(
					new IdentityAppUser { UserName = "user1", Email = "user1@mail.ru", Roles = [new RoleEntity() { Name = "Admin" }] },
					new IdentityAppUser { UserName = "user2", Email = "user2@mail.ru", Roles = [new RoleEntity() { Name = "Manager" }] },
					new IdentityAppUser { UserName = "user3", Email = "user3@mail.ru", Roles = [new RoleEntity() { Name = "User" }] };
		}
	}
}
