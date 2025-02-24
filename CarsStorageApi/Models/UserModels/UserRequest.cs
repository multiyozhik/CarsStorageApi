using System.ComponentModel.DataAnnotations;

namespace CarsStorageApi.Models.UserModels
{
	/// <summary>
	/// Класс данных пользователя, передаваемых клиентом при создании нового пользователя с ролью.
	/// </summary>
	public class UserRequest
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


        /// <summary>
        /// Список наименований ролей пользователя.
        /// </summary>
        public List<string>? RoleNamesList { get; set; }
    }
}
