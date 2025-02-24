using System.ComponentModel.DataAnnotations;

namespace CarsStorageApi.Models.UserModels
{
	/// <summary>
	/// Класс данных пользователя, передаваемых клиентом при регистрации в приложение.
	/// </summary>
	public class RegisterUserRequest
	{
		/// <summary>
		/// Имя пользователя.
		/// </summary>
		[Required(ErrorMessage = "Укажите имя пользователя")]
		public string? UserName { get; set; }


		/// <summary>
		/// Email пользователя.
		/// </summary>
		[Required(ErrorMessage = "Укажите email пользователя")]
		public string? Email { get; set; }


		/// <summary>
		/// Пароль пользователя.
		/// </summary>
		[Required(ErrorMessage = "Укажите пароль пользователя")]
		public string? Password { get; set; }
	}
}
