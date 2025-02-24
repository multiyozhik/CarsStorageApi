using CarsStorage.DAL.Models;

namespace CarsStorage.DAL.Entities
{
	/// <summary>
	/// Класс сущности роли.
	/// </summary>
	public class RoleEntity(string name)
	{
		/// <summary>
		/// Идентификатор роли.
		/// </summary>
		public int RoleEntityId { get; set; }


		/// <summary>
		/// Наименование роли.
		/// </summary>
		public string? Name { get; set; } = name;


		/// <summary>
		/// Список утверждений для роли.
		/// </summary>
		public List<RoleClaimType> RoleClaims { get; set; } = [];


		/// <summary>
		/// Список сущностей пользователей.
		/// </summary>
		public List<UserEntity> UsersList { get; set; } = [];
	}
}
