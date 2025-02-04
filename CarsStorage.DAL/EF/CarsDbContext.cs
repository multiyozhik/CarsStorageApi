using CarsStorage.DAL.Config;
using CarsStorage.DAL.Entities;
using Microsoft.EntityFrameworkCore;


namespace CarsStorage.DAL.EF
{
	/// <summary>
	///  DbContext для таблицы автомобилей. 
	/// </summary>
	/// <param name="options"></param>
	public class CarsDbContext(DbContextOptions<CarsDbContext> options) : DbContext(options)
	{
		public DbSet<CarEntity> Cars => Set<CarEntity>();
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfiguration(new CarsConfig());
		}
	}
}
