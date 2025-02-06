using CarsStorage.DAL.Models;

namespace CarsStorage.DAL.Entities
{
    /// <summary>
    /// Класс сущности роли.
    /// </summary>
    public class RoleEntity(string name)
	{
		public int RoleEntityId { get; set; }

		public string? Name { get; set; } = name;

		public List<RoleClaimType> RoleClaims { get; set; } = [];

		public List<UserEntity> UsersList { get; set; } = [];

		public List<UsersRolesEntity> UserRolesList { get; set; } = [];
	}
}
