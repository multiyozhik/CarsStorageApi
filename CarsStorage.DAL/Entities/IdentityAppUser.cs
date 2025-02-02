using Microsoft.AspNetCore.Identity;

namespace CarsStorage.DAL.Entities
{
    /// <summary>
    /// IdentityAppUser - пользователь в Identity.
    /// </summary>

    public class IdentityAppUser : IdentityUser
	{
		public string? AccessToken { get; set; }
		public string? RefreshToken { get; set; }
		public List<RoleEntity>? RolesList { get; set; } = [];
		public List<UsersRolesEntity> UserRolesList { get; set; } = [];
	}
}
