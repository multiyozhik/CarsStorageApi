using CarsStorage.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarsStorage.DAL.Models
{
	public class UsersConfig(IEnumerable<RoleEntity> rolesList) : IEntityTypeConfiguration<IdentityAppUser>
	{
		private readonly List<RoleEntity> rolesList = rolesList.ToList();		
		private readonly Random random = new();

		public void Configure(EntityTypeBuilder<IdentityAppUser> builder)
		{
			var passwordHasher = new PasswordHasher<IdentityAppUser>();
			var usersList = new List<IdentityAppUser>();
			for (int i = 1; i < 10; i++)
			{
				usersList[i] = new IdentityAppUser
				{
					UserName = $"user{i}",
					Email = $"user{i}@mail.ru",
					RolesList = [rolesList.ToList()[random.Next(rolesList.Count)]]
				};
				usersList[i].PasswordHash = passwordHasher.HashPassword(usersList[i], $"user{i}");
				usersList[i].RolesList = [new RoleEntity("User")];
			}
			builder.HasData(usersList);
			builder.Property(u => u.RolesList).IsRequired();

			builder
				.HasMany(u => u.RolesList)
				.WithMany(r => r.UsersList)
				.UsingEntity<UsersRolesEntity>(
					ur => ur
						.HasOne(i => i.RoleEntity)
						.WithMany(u => u.UserRolesList)
						.HasForeignKey(i => i.IdentityAppUserId), 
					ur => ur
						.HasOne(i => i.IdentityAppUser)
						.WithMany(r => r.UserRolesList)
						.HasForeignKey(i => i.RoleEntityId),
					ur =>
					{
						ur.HasKey(i => new { i.IdentityAppUserId, i.RoleEntityId });
						ur.ToTable("UserRoles");
					});
		}
	}
}
