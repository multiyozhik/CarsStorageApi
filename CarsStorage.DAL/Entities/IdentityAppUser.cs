using Microsoft.AspNetCore.Identity;

namespace CarsStorage.DAL.Entities
{
	/// <summary>
	/// IdentityAppUser - пользователь в Identity.
	/// </summary>

	public class IdentityAppUser : IdentityUser
	{
		public IEnumerable<RoleEntity> RolesList { get; set; } = [];
		public IEnumerable<UsersRolesEntity> UserRolesList { get; set; } = [];
	}
}
