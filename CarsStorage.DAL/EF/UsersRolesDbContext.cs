using CarsStorage.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarsStorage.DAL.EF
{
	/// <summary>
	/// DbContext для таблицы соотношений между пользователем и ролью. 
	/// </summary>
	public class UsersRolesDbContext: DbContext
	{
		private readonly Random random = new();
		public DbSet<IdentityAppUser> UsersId {  get; set; }
		public DbSet<RoleEntity> RolesId { get; set; }
		public DbSet<UsersRolesEntity> UserRoles { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<IdentityAppUser>()
				.HasMany(u => u.RolesList)
				.WithMany(r => r.UsersList)
				.UsingEntity("UsersRolesJoinTable");
			
			var usersRolesEntities = new List<UsersRolesEntity>(); 
			for (var i = 1; i < 10; i++)
			{
				usersRolesEntities.Add(new UsersRolesEntity(i, random.Next(1, 4)));
			}
			modelBuilder.Entity<RoleEntity>().HasData(usersRolesEntities);
		}
	}
}
