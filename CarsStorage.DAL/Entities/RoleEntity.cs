using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsStorage.DAL.Entities
{
	public class RoleEntity
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		[StringLength(50)]
		public string? Name { get; set; }

		public IEnumerable<IdentityAppUser> Users { get; set; } = [];

		[DefaultValue(false)]
		public bool CanBrowseCars { get; set; }

		[DefaultValue(false)]
		public bool CanManageCars { get; set; }

		[DefaultValue(false)]
		public bool CanManageUsers { get; set; }
	}
}
