using CarsStorage.DAL.Entities;
using CarsStorage.DAL.Models;
using Microsoft.EntityFrameworkCore;


namespace CarsStorage.DAL.EF
{
	/// <summary>
	///  DbContext для таблицы автомобилей. 
	/// </summary>
	/// <param name="options"></param>
	public class CarsAppDbContext(DbContextOptions<CarsAppDbContext> options) : DbContext(options)
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
