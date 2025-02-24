namespace CarsStorage.Abstractions.ModelsDTO.User
{
	/// <summary>
	/// Класс, представляющий данные о пользователе, аутентифицированном на стороннем провайдере аутентификации.
	/// </summary>
	public class AuthUserDataDTO
	{
		/// <summary>
		/// Имя пользователя.
		/// </summary>
		public string? UserName { get; set; }


		/// <summary>
		/// Email пользователя.
		/// </summary>
		public string? Email { get; set; }


		/// <summary>
		/// Список наименований ролей пользователя.
		/// </summary>
		public List<string> RolesNamesList { get; set; } = [];
	}
}
