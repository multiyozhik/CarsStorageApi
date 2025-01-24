using CarsStorage.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarsStorage.DAL.EF
{
	/// <summary>
	/// DbContext для таблицы соотношений между пользователем и ролью. 
	/// </summary>
	public class UsersRolesDbContext: DbContext
	{		
		public DbSet<IdentityAppUser> IdentityAppUsers {  get; set; }
		public DbSet<RoleEntity> Roles { get; set; }
		public DbSet<UsersRolesEntity> UserRoles { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<IdentityAppUser>()
				.HasMany(u => u.RolesList)
				.WithMany(r => r.UsersList)
				.UsingEntity("UsersRolesJoinTable");

			modelBuilder.Entity<IdentityAppUser>();
		}
	}
}
