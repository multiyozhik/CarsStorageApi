using CarsStorage.DAL.Models;

namespace CarsStorage.DAL.Entities
{
    /// <summary>
    /// Сущность роли.
    /// </summary>
    /// <param name="name">Имя роли.</param>

    public class RoleEntity(string name)
	{
		public int Id { get; set; }

		public string? Name { get; set; } = name;

		public List<RoleClaimType> RoleClaims { get; set; } = [];

		public List<AppUserEntity> UsersList { get; set; } = [];

		public List<UsersRolesEntity> UserRolesList { get; set; } = [];
	}
}
