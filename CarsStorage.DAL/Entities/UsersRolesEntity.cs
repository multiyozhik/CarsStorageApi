using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsStorage.DAL.Entities
{
	/// <summary>
	/// Сущность в таблице соотношений между пользователем и ролью.
	/// </summary>
	/// <param name="userId">Id пользователя.</param>
	/// <param name="roleId">Id роли у данного пользователя.</param>
	public class UsersRolesEntity(int userId, int roleId)
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public int? UserId { get; set; } = userId;
		public int? RoleId { get; set; } = roleId;
	}
}
