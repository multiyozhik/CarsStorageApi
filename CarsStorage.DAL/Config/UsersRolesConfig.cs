using CarsStorage.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarsStorage.DAL.Config
{
	/// <summary>
	/// Класс определяет конфигурацию для UsersRolesEntity сущности. 
	/// </summary>
	public class UsersRolesConfig() : IEntityTypeConfiguration<UsersRolesEntity>
	{
		public void Configure(EntityTypeBuilder<UsersRolesEntity> builder)
		{
			builder
			   .HasOne(e => e.UserEntity)
			   .WithMany(s => s.UserRolesList)
			   .HasForeignKey(e => e.UserEntityId);

			builder
				.HasOne(e => e.RoleEntity)
				.WithMany(c => c.UserRolesList)
				.HasForeignKey(e => e.RoleEntityId);

			builder.HasData(
				new UsersRolesEntity { UserEntityId = 1, RoleEntityId = 1 },
				new UsersRolesEntity { UserEntityId = 2, RoleEntityId = 2 },
				new UsersRolesEntity { UserEntityId = 3, RoleEntityId = 2 },
				new UsersRolesEntity { UserEntityId = 4, RoleEntityId = 3 },
				new UsersRolesEntity { UserEntityId = 5, RoleEntityId = 3 });

			builder.HasKey("UserEntityId", "RoleEntityId");
			builder.ToTable("UsersRoles");
		}
	}
}
