namespace CarsStorage.BLL.Abstractions.ModelsDTO.UserDTO
{
	public class UserCreaterDTO
	{
		public string? UserName { get; set; }
		public string? Email { get; set; }
		public string? Password { get; set; }
		public List<string>? Roles { get; set; }
	}
}
