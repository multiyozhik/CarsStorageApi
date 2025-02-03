using CarsStorage.DAL.Entities;
using CarsStorage.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CarsStorage.DAL.EF
{
	/// <summary>
	///  DbContext для таблицы пользователей в Identity. 
	/// </summary>
	/// <param name="options"></param>
	public class IdentityAppDbContext(DbContextOptions<IdentityAppDbContext> options, IOptions<InitialDbSeedConfig> initialOptions, IPasswordHasher<IdentityAppUser> passwordHasher) : IdentityDbContext<IdentityAppUser>(options)
	{
		public DbSet<IdentityAppUser> IdentityAppUsers { get; set; }
		public new DbSet<RoleEntity> Roles { get; set; }
		public new DbSet<UsersRolesEntity> UserRoles { get; set; }

		private readonly InitialDbSeedConfig initialDbSeedConfig = initialOptions.Value;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			var rolesConfig = new RolesConfig();
			modelBuilder.ApplyConfiguration(rolesConfig);
			modelBuilder.ApplyConfiguration(new UsersConfig(rolesConfig.GetRoles(), initialDbSeedConfig, passwordHasher));
		}
	}
}
