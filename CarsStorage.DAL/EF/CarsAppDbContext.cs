using CarsStorage.DAL.Entities;
using Microsoft.EntityFrameworkCore;


namespace CarsStorage.DAL.EF
{
	public class CarsAppDbContext(DbContextOptions<CarsAppDbContext> options) : DbContext(options)
	{
		public DbSet<CarRow> Cars => Set<CarRow>();
	}
}
