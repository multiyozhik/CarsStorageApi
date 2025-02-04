using CarsStorage.DAL.Config;
using CarsStorage.DAL.Entities;
using CarsStorage.DAL.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CarsStorage.DAL.EF
{
    /// <summary>
    ///  DbContext для таблицы пользователей. 
    /// </summary>
    /// <param name="options"></param>
    public class UsersRolesDbContext(DbContextOptions<UsersRolesDbContext> options, IPasswordHasher passwordHasher, 
		IOptions<InitialDbSeedConfig> initialOptions, IOptions<AdminConfig> adminOptions) : DbContext(options)
	{
		public DbSet<UserEntity> Users { get; set; }
		public DbSet<RoleEntity> Roles { get; set; }
		public DbSet<UsersRolesEntity> UserRoles { get; set; }

		private readonly InitialDbSeedConfig initialDbSeedConfig = initialOptions.Value;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			var rolesConfig = new RolesConfig();
			modelBuilder.ApplyConfiguration(rolesConfig);
			modelBuilder.ApplyConfiguration(new UsersConfig(rolesConfig.GetRoles(), initialDbSeedConfig, passwordHasher, adminOptions));
		}
	}
}
