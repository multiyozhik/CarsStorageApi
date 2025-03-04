namespace CarsStorage.Abstractions.ModelsDTO.User
{
	/// <summary>
	/// Класс, представляющий данные пользователя для входа в приложение.
	/// </summary>
	public class UserLoginDTO
	{
		/// <summary>
		/// Имя пользователя.
		/// </summary>
		public string UserName { get; set; } = string.Empty;


		/// <summary>
		/// Пароль пользователя.
		/// </summary>
		public string Password { get; set; } = string.Empty;
	}
}
