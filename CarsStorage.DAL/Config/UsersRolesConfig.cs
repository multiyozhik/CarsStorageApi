using CarsStorage.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarsStorage.DAL.Config
{
	/// <summary>
	/// Класс определяет конфигурацию для UsersRolesEntity сущности. 
	/// </summary>
	public class UsersRolesConfig(InitialDbSeedConfig initialDbSeedConfig) : IEntityTypeConfiguration<UsersRolesEntity>
	{
		private readonly List<UsersRolesEntity> usersRolesList = GetUsersRolesEntities(initialDbSeedConfig);
		private static List<UsersRolesEntity> GetUsersRolesEntities(InitialDbSeedConfig initialDbSeedConfig)
		{
			var usersRolesList = Enumerable.Range(3, initialDbSeedConfig.InitialUsersCount)
				.Select(index => new UsersRolesEntity
				{
					UserEntityId = index,
					RoleEntityId = 3
				}).ToList();
			usersRolesList.Add(new UsersRolesEntity { UserEntityId = 1, RoleEntityId = 1 });
			usersRolesList.Add(new UsersRolesEntity { UserEntityId = 2, RoleEntityId = 2 });
			return usersRolesList;
		}

		public void Configure(EntityTypeBuilder<UsersRolesEntity> builder)
		{
			builder.HasData(usersRolesList);
		}
	}
}
