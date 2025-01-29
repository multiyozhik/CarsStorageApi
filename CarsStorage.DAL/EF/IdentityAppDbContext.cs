using CarsStorage.DAL.Entities;
using CarsStorage.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarsStorage.DAL.EF
{
	/// <summary>
	///  DbContext для таблицы пользователей в Identity. 
	/// </summary>
	/// <param name="options"></param>
	public class IdentityAppDbContext(DbContextOptions<IdentityAppDbContext> options) : IdentityDbContext<IdentityAppUser>(options)
	{
		public DbSet<IdentityAppUser> IdentityAppUsers { get; set; }
		public new DbSet<RoleEntity> Roles { get; set; }
		public new DbSet<UsersRolesEntity> UserRoles { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			var rolesConfig = new RolesConfig();
			modelBuilder.ApplyConfiguration(rolesConfig);
			modelBuilder.ApplyConfiguration(new UsersConfig(rolesConfig.GetRoles()));
		}
	}
}
