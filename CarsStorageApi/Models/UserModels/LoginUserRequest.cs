using System.ComponentModel.DataAnnotations;

namespace CarsStorageApi.Models.UserModels
{
	/// <summary>
	/// Класс данных пользователя, передаваемых клиентом при входе в приложение.
	/// </summary>
	public class LoginUserRequest
	{
		[Required(ErrorMessage = "Укажите имя пользователя")]
		public string? UserName { get; set; }

		[Required(ErrorMessage = "Укажите пароль пользователя")]
		public string? Password { get; set; }
	}
}
