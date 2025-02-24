namespace CarsStorage.Abstractions.ModelsDTO.User
{
	/// <summary>
	/// Класс, представляющий данные пользователя для создания пользователя.
	/// </summary>
	public class UserUpdaterDTO
	{
		/// <summary>
		/// Идентификатор пользователя.
		/// </summary>
		public int Id { get; set; }


		/// <summary>
		/// Имя пользователя.
		/// </summary>
		public string UserName { get; set; } = string.Empty;


		/// <summary>
		/// Email пользователя.
		/// </summary>
		public string Email { get; set; } = string.Empty;


		/// <summary>
		/// Список наименований ролей пользователя.
		/// </summary>
		public List<string>? RoleNamesList { get; set; } = [];
	}
}
