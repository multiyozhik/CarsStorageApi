using CarsStorage.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarsStorage.DAL.EF
{
	public class RolesDbContext(DbContextOptions<RolesDbContext> options) : DbContext(options)
	{
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<RoleEntity>().HasData(
					new RoleEntity { Id = 1, Name = "Admin", CanManageUsers = true },
					new RoleEntity { Id = 2, Name = "Manager", CanManageCars = true, CanBrowseCars = true },
					new RoleEntity { Id = 3, Name = "User", CanBrowseCars = true });
		}
	}
}
