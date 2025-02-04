namespace CarsStorage.BLL.Abstractions.ModelsDTO.UserDTO
{
	public class UserDTO
	{
		public int Id { get; set; }
		public string? UserName { get; set; }
		public string? Email { get; set; }
		public List<string>? Roles { get; set; }
		public string? AccessToken { get; set; }
		public string? RefreshToken { get; set; }
	}
}
