using System.ComponentModel.DataAnnotations;

namespace CarsStorageApi.AuthModels
{
    public class LoginDTO
    {
		[Required(ErrorMessage = "Укажите имя пользователя")]
		public string? UserName { get; set; }

		[Required(ErrorMessage = "Укажите пароль пользователя")]
		public string? Password { get; set; }
    }
}
