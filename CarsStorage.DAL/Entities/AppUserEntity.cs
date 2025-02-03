using Microsoft.AspNetCore.Identity;

namespace CarsStorage.DAL.Entities
{
    /// <summary>
    /// AppUserEntity - пользователь в Identity.
    /// </summary>

    public class AppUserEntity
	{
		public int Id { get; set; }
		public string? UserName { get; set; }
		public string? Email { get; set; }
		public string? AccessToken { get; set; }
		public string? RefreshToken { get; set; }
		public List<RoleEntity>? RolesList { get; set; } = [];
		public List<UsersRolesEntity> UserRolesList { get; set; } = [];
	}
}
