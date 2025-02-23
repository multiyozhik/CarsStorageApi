namespace CarsStorage.Abstractions.ModelsDTO.User
{
	/// <summary>
	/// Класс данных пользователя с именем и паролем при входе в приложение.
	/// </summary>
	public class UserLoginDTO
	{
		public string UserName { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
	}
}
