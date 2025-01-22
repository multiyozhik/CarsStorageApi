using CarsStorage.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarsStorage.DAL.EF
{
	/// <summary>
	/// DbContext для таблицы ролей. 
	/// </summary>
	/// <param name="options"></param>
	public class RolesDbContext(DbContextOptions<RolesDbContext> options) : DbContext(options)
	{
		public DbSet<RoleEntity> Roles { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			var adminRole = new RoleEntity("Admin")
			{
				Id = 1,
				RoleClaims = new Dictionary<RoleClaimType, bool>() {
					{ RoleClaimType.CanManageUsers, true },
					{ RoleClaimType.CanManageRoles, true }
				}
			};
			var managerRole = new RoleEntity("Manager")
			{
				Id = 2,
				RoleClaims = new Dictionary<RoleClaimType, bool>() {
					{ RoleClaimType.CanManageCars, true },
					{ RoleClaimType.CanBrowseCars, true }
				}
			};
			var userRole = new RoleEntity("User")
			{
				Id = 3,
				RoleClaims = new Dictionary<RoleClaimType, bool>() {
					{ RoleClaimType.CanBrowseCars, true }
				}
			};

			modelBuilder.Entity<RoleEntity>().HasData(adminRole, managerRole, userRole);
		}
	}
}
