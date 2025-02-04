using System.ComponentModel.DataAnnotations;

namespace CarsStorageApi.Models.AuthModels
{
    /// <summary>
    /// Класс для данных пользователя при входе в приложение.
    /// </summary>
    public class LoginDataRequest
    {
        [Required(ErrorMessage = "Укажите имя пользователя")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Укажите пароль пользователя")]
        public string? Password { get; set; }
    }
}
