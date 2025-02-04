using System.ComponentModel.DataAnnotations;

namespace CarsStorageApi.Models.UserModels
{
	/// <summary>
	/// Класс данных пользователя при регистрации в приложении (с id и ролями, без пароля).
	/// </summary>
	public class UserRequestResponse
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public IEnumerable<string>? Roles { get; set; }
    }
}
