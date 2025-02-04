using System.ComponentModel.DataAnnotations;

namespace CarsStorageApi.Models.AuthModels
{
	/// <summary>
	/// Класс для данных пользователя при регистрации его в приложении.
	/// </summary>
	public class RegisterUserDataRequest
    {
        [Required(ErrorMessage = "Укажите имя пользователя")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Укажите email пользователя")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Укажите пароль пользователя")]
        public string? Password { get; set; }
    }
}
