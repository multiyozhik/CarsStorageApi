using CarsStorage.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarsStorage.DAL.Config
{
	/// <summary>
	/// Класс конфигурации промежуточной сущности UsersRolesEntity.
	/// </summary>
	public class UsersRolesConfig() : IEntityTypeConfiguration<UsersRolesEntity>
	{
		/// <summary>
		/// Метод конфигурирования промежуточной сущности UsersRolesEntity.
		/// </summary>
		/// <param name="builder">Объект API для конфигурирования.</param>
		public void Configure(EntityTypeBuilder<UsersRolesEntity> builder)
		{			
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
