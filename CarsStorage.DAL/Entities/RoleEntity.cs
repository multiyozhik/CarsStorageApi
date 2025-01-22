using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsStorage.DAL.Entities
{
	/// <summary>
	/// Сущность роли.
	/// </summary>
	/// <param name="name">Имя роли.</param>
	public class RoleEntity(string name)
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		[StringLength(50)]
		public string? Name { get; set; } = name;

		public required Dictionary<RoleClaimType, bool> RoleClaims { get; init; }

		public IEnumerable<IdentityAppUser>? UsersList { get; set; }
	}
}
