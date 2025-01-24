using CarsStorage.DAL.Entities;
using CarsStorage.DAL.Models;
using Microsoft.AspNetCore.Identity;
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
			var rolesList = new List<RoleEntity>
			{
				[0] = new RoleEntity("admin") { RoleClaims = [RoleClaimType.CanManageUsers, RoleClaimType.CanManageRoles] },
				[1] = new RoleEntity("manager") { RoleClaims = [RoleClaimType.CanManageUsers, RoleClaimType.CanManageRoles] },
				[2] = new RoleEntity("user") { RoleClaims = [RoleClaimType.CanManageUsers, RoleClaimType.CanManageRoles] }
			};

			modelBuilder.Entity<IdentityAppUser>().HasData(rolesList);
		}
	}
}
