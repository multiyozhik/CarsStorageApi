using System.ComponentModel.DataAnnotations;

namespace CarsStorageApi
{
	public class UserDTO
	{
		public Guid Id {  get; set; }
		public string? UserName { get; set; }
		public string? Email { get; set; }
		public IEnumerable<string>? Roles { get; set; }
	}
}
