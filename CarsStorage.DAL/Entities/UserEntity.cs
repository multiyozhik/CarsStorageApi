namespace CarsStorage.DAL.Entities
{
	/// <summary>
	/// Класс сущности пользователя.
	/// </summary>
	public class UserEntity
	{
		/// <summary>
		/// Идентификатор пользователя.
		/// </summary>
		public int UserEntityId { get; set; }


		/// <summary>
		/// Имя пользователя.
		/// </summary>
		public string? UserName { get; set; }


		/// <summary>
		/// Email пользователя.
		/// </summary>
		public string? Email { get; set; }


		/// <summary>
		/// Хеш пароля.
		/// </summary>
		public string? Hash { get; set; }


		/// <summary>
		/// Соль пароля.
		/// </summary>
		public string? Salt { get; set; }


		/// <summary>
		/// Строка токена доступа.
		/// </summary>
		public string? AccessToken { get; set; }


		/// <summary>
		/// Строка токена обновления.
		/// </summary>
		public string? RefreshToken { get; set; }


		/// <summary>
		/// Список объектов ролей пользователя.
		/// </summary>
		public List<RoleEntity>? RolesList { get; set; } = [];
	}
}
