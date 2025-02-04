using CarsStorage.DAL.Config;
using CarsStorage.DAL.Entities;
using CarsStorage.DAL.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CarsStorage.DAL.EF
{
	/// <summary>
	///  DbContext для пользователей, их ролей и отношений между ними. 
	/// </summary>
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
			modelBuilder.ApplyConfiguration(new UsersConfig(passwordHasher, initialDbSeedConfig, adminOptions));
			modelBuilder.ApplyConfiguration(new UsersRolesConfig(initialDbSeedConfig));
		}
	}
}
