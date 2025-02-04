namespace CarsStorage.DAL.Models
{
	/// <summary>
	/// Класс для возврата данных пользователя с его ролями (не показываем пароль) после регистрации и создании записис в БД.
	/// </summary>
	public class UserRegister
	{
		public int Id { get; set; }
		public string? UserName { get; set; }
		public string? Email { get; set; }
		public List<string>? RolesList { get; set; } = [];
	}
}
