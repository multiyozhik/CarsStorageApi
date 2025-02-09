﻿namespace CarsStorage.DAL.Entities
{
	/// <summary>
	/// Класс сущности пользователя.
	/// </summary>
	public class UserEntity
	{
		public int UserEntityId { get; set; }
		public string? UserName { get; set; }
		public string? Email { get; set; }
		public string? Hash { get; set; }
		public string? Salt { get; set; }
		public string? AccessToken { get; set; }
		public string? RefreshToken { get; set; }
		public List<RoleEntity>? RolesList { get; set; } = [];
		public List<UsersRolesEntity> UserRolesList { get; set; } = [];
	}
}
