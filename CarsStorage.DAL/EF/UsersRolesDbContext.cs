using CarsStorage.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsStorage.DAL.EF
{
	public class UsersRolesDbContext: DbContext
	{
		public DbSet<IdentityAppDbContext> Users {  get; set; }
		public DbSet<RoleEntity> Roles { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<IdentityAppUser>()
				.HasMany(r => r.Roles)
				.WithMany(u => u.Users)
				.UsingEntity("UsersRolesJoinTable");
		}
	}
}
