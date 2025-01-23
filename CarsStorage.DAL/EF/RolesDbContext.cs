using CarsStorage.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarsStorage.DAL.EF
{
	/// <summary>
	/// DbContext для таблицы ролей. 
	/// </summary>
	/// <param name="options"></param>
	public class RolesDbContext(DbContextOptions<RolesDbContext> options) : DbContext(options)
	{
		public DbSet<RoleEntity> Roles { get; set; }
	}
}
