using CarsStorage.DAL.Config;
using CarsStorage.DAL.Entities;
using Microsoft.EntityFrameworkCore;


namespace CarsStorage.DAL.EF
{
	/// <summary>
	///  DbContext для приложения, для автомобилей. 
	/// </summary>
	public class CarsDbContext(DbContextOptions<CarsDbContext> options) : DbContext(options)
	{
		public DbSet<CarEntity> Cars => Set<CarEntity>();
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfiguration(new CarsConfig());
		}
	}
}
