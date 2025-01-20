using System.ComponentModel.DataAnnotations;

namespace CarsStorageApi.Config
{
    public class RoleNamesConfig
    {
		[Required]
		public IEnumerable<string>? DefaultUserRoleNames { get; set; }

		[Required]
		public IEnumerable<string>? DefaultRoleNamesList { get; set; }
	}
}
