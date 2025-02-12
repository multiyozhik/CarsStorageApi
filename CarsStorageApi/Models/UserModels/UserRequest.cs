using System.ComponentModel.DataAnnotations;

namespace CarsStorageApi.Models.UserModels
{
	/// <summary>
	/// Класс данных пользователя, передаваемых клиентом при создании нового пользователя с ролью.
	/// </summary>
	public class UserRequest
    {
        [Required(ErrorMessage = "Укажите имя пользователя")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Укажите email пользователя")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Укажите пароль пользователя")]
        public string? Password { get; set; }
        public List<string>? RoleNamesList { get; set; }
    }
}
