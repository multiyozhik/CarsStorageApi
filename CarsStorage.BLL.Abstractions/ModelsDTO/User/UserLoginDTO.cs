namespace CarsStorage.BLL.Abstractions.ModelsDTO.User
{
	/// <summary>
	/// Класс данных пользователя с именем и паролем при входе в приложение.
	/// </summary>
	public class UserLoginDTO
	{
		public string? UserName { get; set; }
		public string? Password { get; set; }
	}
}
