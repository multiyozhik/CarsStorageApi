
namespace CarsStorage.Abstractions.ModelsDTO.User
{
	/// <summary>
	/// Класс, представляющий данные пользователя для создания пользователя.
	/// </summary>
	public class UserCreaterDTO
	{
		/// <summary>
		/// Имя пользователя.
		/// </summary>
		public string UserName { get; set; } = string.Empty;


		/// <summary>
		/// Email пользователя.
		/// </summary>
		public string Email { get; set; } = string.Empty;


		/// <summary>
		/// Пароль пользователя.
		/// </summary>
		public string Password { get; set; } = string.Empty;


		/// <summary>
		/// Список наименований ролей пользователя.
		/// </summary>
		public List<string> RoleNamesList { get; set; } = [];
	}
}
