using CarsStorage.DAL.Config;
using CarsStorage.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarsStorage.DAL.DbContexts
{
	/// <summary>
	/// Класс контекста данных приложения. 
	/// </summary>
	/// <param name="options">Параметры, используемые контекстом данных.</param>
	public class AppDbContext(DbContextOptions<AppDbContext> options): DbContext(options)
	{
		/// <summary>
		/// Набор пользователей, хранящихся в БД.
		/// </summary>
		public DbSet<UserEntity> Users { get; set; }

		/// <summary>
		/// Набор пользователей, хранящихся в БД.
		/// </summary>
		public DbSet<RoleEntity> Roles { get; set; }

		/// <summary>
		/// Набор ролей пользователей, хранящихся в БД.
		/// </summary>
		public DbSet<UsersRolesEntity> UsersRoles { get; set; }

		/// <summary>
		/// Набор автомобилей, хранящихся в БД.
		/// </summary>
		public DbSet<CarEntity> Cars { get; set; }


		/// <summary>
		/// Набор состояний (конфигураций) БД.
		/// </summary>
		public DbSet<DbStateEntity> DbStates { get; set; }


		/// <summary>
		/// Метод для конфигурирования и инициализации БД.
		/// </summary>
		/// <param name="modelBuilder">Объект API конфигурирования.</param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfiguration(new RolesConfig());
			modelBuilder.ApplyConfiguration(new UsersConfig());
			modelBuilder.ApplyConfiguration(new UsersRolesConfig());
			modelBuilder.ApplyConfiguration(new CarsConfig());
			modelBuilder.ApplyConfiguration(new DbStatesConfig());
		}
	}
}
