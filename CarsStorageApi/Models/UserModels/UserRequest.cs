﻿using System.ComponentModel.DataAnnotations;

namespace CarsStorageApi.Models.UserModels
{
	/// <summary>
	/// Класс данных пользователя при регистрации в приложении (без id, с паролем и ролями).
	/// </summary>
	public class UserRequest
    {
        [Required(ErrorMessage = "Укажите имя пользователя")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Укажите email пользователя")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Укажите пароль пользователя")]
        public string? Password { get; set; }
        public IEnumerable<string>? Roles { get; set; }
    }
}
