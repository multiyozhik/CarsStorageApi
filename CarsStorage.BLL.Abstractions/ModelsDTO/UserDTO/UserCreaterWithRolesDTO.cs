namespace CarsStorage.BLL.Abstractions.ModelsDTO.UserDTO
{
	/// <summary>
	/// Класс с данными при регистрации пользователя (без ролей).
	/// </summary>
	public class UserCreaterWithRolesDTO
	{
		public int Id { get; set; }
		public string? UserName { get; set; }
		public string? Email { get; set; }
		public List<string>? Roles { get; set; }
	}
}
