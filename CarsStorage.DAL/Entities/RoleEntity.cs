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

		public IEnumerable<RoleClaimType> RoleClaims { get; set; } = [];

		public IEnumerable<IdentityAppUser> UsersList { get; set; } = [];

		public IEnumerable<UsersRolesEntity> UserRolesList { get; set; } = [];
	}
}
