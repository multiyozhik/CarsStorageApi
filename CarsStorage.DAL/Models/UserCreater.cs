namespace CarsStorage.DAL.Models
{
	/// <summary>
	/// Класс данных пользователя с паролем и ролями для создания записи в БД.
	/// </summary>
	public class UserCreater
	{
		public string? UserName { get; set; }
		public string? Email { get; set; }
		public string? Password { get; set; }
		public IEnumerable<string>? Roles { get; set; }
	}
}
