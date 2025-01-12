using CarsStorage.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;


namespace CarsStorage.DAL.EF
{
	public class CarsAppDbContext: DbContext
	{
		public DbSet<CarRow>? Cars => Set<CarRow>();
		
		//public DbSet<UsersModel>? Users { get; set; }

		public CarsAppDbContext(DbContextOptions<CarsAppDbContext> options)
			: base(options)
		{ 
		}

	
	}
}
