using Microsoft.AspNetCore.Identity;

namespace CarsStorage.DAL.Entities
{
    /// <summary>
    /// IdentityAppUser - пользователь в Identity.
    /// </summary>

    public class IdentityAppUser : IdentityUser
	{
		public List<RoleEntity>? RolesList { get; set; } = [];
		public List<UsersRolesEntity> UserRolesList { get; set; } = [];
	}
}
