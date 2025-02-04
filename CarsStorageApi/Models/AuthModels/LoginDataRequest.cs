using System.ComponentModel.DataAnnotations;

namespace CarsStorageApi.Models.AuthModels
{
    public class LoginDataRequest
    {
        [Required(ErrorMessage = "Укажите имя пользователя")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Укажите пароль пользователя")]
        public string? Password { get; set; }
    }
}
