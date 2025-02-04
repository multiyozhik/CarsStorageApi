namespace CarsStorage.BLL.Abstractions.ModelsDTO.UserDTO
{
	/// <summary>
	/// Класс с данными для создания пользователя с ролями.
	/// </summary>
	public class UserCreaterDTO
	{
		public string? UserName { get; set; }
		public string? Email { get; set; }
		public string? Password { get; set; }
		public List<string>? Roles { get; set; }
	}
}
