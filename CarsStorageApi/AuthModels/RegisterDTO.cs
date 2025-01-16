using System.ComponentModel.DataAnnotations;

namespace CarsStorageApi.ModelsDto
{
    public class RegisterDTO
    {
		[Required(ErrorMessage = "Укажите имя пользователя")]
		public string? UserName { get; set; }

		[Required(ErrorMessage = "Укажите email пользователя")]
		public string? Email { get; set; }

		[Required(ErrorMessage = "Укажите пароль пользователя")]
		public string? Password { get; set; }
    }
}
