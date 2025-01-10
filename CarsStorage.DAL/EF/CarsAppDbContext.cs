using CarsStorage.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;


namespace CarsStorage.DAL.EF
{
	public class CarsAppDbContext: DbContext
	{
		private readonly IConfiguration config;
		public DbSet<CarRow>? Cars { get; set; }
		
		//public DbSet<UsersModel>? Users { get; set; }

		public CarsAppDbContext(IConfiguration configuration)
		{
			config = configuration;
		}

		protected override void  OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseNpgsql(config["NpgConnection"]);
		}
	}
}
