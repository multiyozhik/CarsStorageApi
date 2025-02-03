using CarsStorage.DAL.Entities;
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
			modelBuilder.Entity<CarEntity>().HasData(
					new CarEntity { Id = 1, Model = "Lada", Make = "Kalina", Color = "красный", Count = 4 },
					new CarEntity { Id = 2, Model = "JAC", Make = "J7", Color = "белый", Count = 8 },
					new CarEntity { Id = 3, Model = "Lada", Make = "Granta", Color = "синий", Count = 6 },
					new CarEntity { Id = 4, Model = "Audi", Make = "G8", Color = "черный", Count = 5 },
					new CarEntity { Id = 5, Model = "Cherry", Make = "Tigo 4", Color = "серый", Count = 2 }
			);
		}
	}
}
