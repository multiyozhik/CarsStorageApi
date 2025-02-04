using Microsoft.AspNetCore.Identity;

namespace CarsStorage.DAL.Entities
{
    /// <summary>
    /// Класс сущности пользователя.
    /// </summary>

    public class UserEntity
	{
		public int Id { get; set; }
		public string? UserName { get; set; }
		public string? Email { get; set; }
		public (string hash, string salt) PasswordHash { get; set; }
		public string? AccessToken { get; set; }
		public string? RefreshToken { get; set; }
		public List<RoleEntity>? RolesList { get; set; } = [];
		public List<UsersRolesEntity> UserRolesList { get; set; } = [];
	}
}
