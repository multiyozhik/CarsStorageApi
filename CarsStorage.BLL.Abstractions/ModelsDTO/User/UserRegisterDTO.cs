namespace CarsStorage.BLL.Abstractions.ModelsDTO.User
{
	/// <summary>
	///  Класс всех данных пользователя с именем и паролем для регистрации в приложение.
	/// </summary>
	public class UserRegisterDTO
	{
		public string? UserName { get; set; }
		public string? Email { get; set; }
		public string? Password { get; set; }
	}
}
