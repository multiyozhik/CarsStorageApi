using CarsStorage.DAL.Config;
using CarsStorage.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarsStorage.DAL.DbContexts
{
	/// <summary>
	///  DbContext приложения. 
	/// </summary>
	public class AppDbContext(DbContextOptions<AppDbContext> options): DbContext(options)
	{
		public DbSet<UserEntity> Users { get; set; }
		public DbSet<RoleEntity> Roles { get; set; }
		public DbSet<UsersRolesEntity> UserRoles { get; set; }
		public DbSet<CarEntity> Cars { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfiguration(new RolesConfig());
			modelBuilder.ApplyConfiguration(new UsersConfig());
			modelBuilder.ApplyConfiguration(new UsersRolesConfig());
			modelBuilder.ApplyConfiguration(new CarsConfig());
		}
	}
}
