using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsStorage.DAL.Entities
{
	/// <summary>
	/// Сущность в таблице отношений между пользователем и ролью.
	/// </summary>

	public class UsersRolesEntity
	{
		public int IdentityAppUserId { get; set; }
		public int RoleEntityId { get; set; }

		public IdentityAppUser? IdentityAppUser { get; set; }

		public RoleEntity? RoleEntity { get; set; }
	}
}
