using CarsStorage.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;
using System.Reflection.Emit;

namespace CarsStorage.DAL.Models
{
    public class UsersConfig(IEnumerable<RoleEntity> rolesList, InitialDbSeedConfig initialDbSeedConfig) : IEntityTypeConfiguration<IdentityAppUser>
	{
		private readonly List<RoleEntity> rolesList = rolesList.ToList();
		private readonly PasswordHasher<IdentityAppUser> passwordHasher = new();
		private readonly Random random = new();

		public void Configure(EntityTypeBuilder<IdentityAppUser> builder)
		{
			var usersList = Enumerable.Range(1, initialDbSeedConfig.InitialUsersCount)
				.Select(index =>
				{
					var user = new IdentityAppUser()
					{
						UserName = $"user{index}",
						Email = $"user{index}@mail.ru",
						RolesList = [rolesList[random.Next(rolesList.Count)]]
					};
					user.PasswordHash = passwordHasher.HashPassword(user, $"user{index}");
					return user;
				});			
			builder.HasData(usersList);

			builder.HasKey(u => u.Id);
			builder.ToTable("Users");
			
			//builder.Property(u => u.Roles).IsRequired();  //после удаления этой строки ошибка про уже существование RolesList ушла

			builder
				.HasMany(u => u.RolesList)
				.WithMany(r => r.UsersList)
				.UsingEntity<UsersRolesEntity>(
					ur => ur
						.HasOne(i => i.RoleEntity)
						.WithMany(u => u.UserRolesList)
						.HasForeignKey(i => i.RoleEntityId)
						.OnDelete(DeleteBehavior.Cascade),
					ur => ur
						.HasOne(i => i.IdentityAppUser)
						.WithMany(r => r.UserRolesList)
						.HasForeignKey(i => i.IdentityAppUserId)
						.OnDelete(DeleteBehavior.Cascade),
					ur =>
					{
						ur.HasKey(i => new { i.IdentityAppUserId, i.RoleEntityId });
						ur.ToTable("UserRoles");
					});
		}
	}
}
