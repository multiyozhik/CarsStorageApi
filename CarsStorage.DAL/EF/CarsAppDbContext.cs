using CarsStorage.DAL.Entities;
using Microsoft.EntityFrameworkCore;


namespace CarsStorage.DAL.EF
{
	public class CarsAppDbContext: DbContext
	{
		public DbSet<CarRow>? Cars => Set<CarRow>();
	

		public CarsAppDbContext(DbContextOptions<CarsAppDbContext> options)
			: base(options)
		{ 
		}

	
	}
}
