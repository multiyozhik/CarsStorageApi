using System.ComponentModel.DataAnnotations;

namespace CarsStorageApi.Models.UserModels
{
	/// <summary>
	/// Класс данных пользователя, передаваемых клиентом при входе в приложение.
	/// </summary>
	public class LoginUserRequest
	{
		/// <summary>
		/// Имя пользователя.
		/// </summary>
		[Required(ErrorMessage = "Укажите имя пользователя")]
		public string? UserName { get; set; }


		/// <summary>
		/// Пароль пользователя.
		/// </summary>
		[Required(ErrorMessage = "Укажите пароль пользователя")]
		public string? Password { get; set; }
	}
}
