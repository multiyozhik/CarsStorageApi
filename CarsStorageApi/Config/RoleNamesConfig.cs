using System.ComponentModel.DataAnnotations;

namespace CarsStorageApi.Config
{
    public class RoleNamesConfig
    {
		[Required]
		public string? DefaultUserRoleName { get; set; }

		[Required]
		public IEnumerable<string>? DefaultRoleNamesList { get; set; }
    }
}
