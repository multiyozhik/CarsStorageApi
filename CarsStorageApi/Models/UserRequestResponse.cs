using System.ComponentModel.DataAnnotations;

namespace CarsStorageApi
{
	public class UserRequestResponse
	{
		public int Id {  get; set; }
		public string? UserName { get; set; }
		public string? Email { get; set; }
		public IEnumerable<string>? Roles { get; set; }
	}
}
