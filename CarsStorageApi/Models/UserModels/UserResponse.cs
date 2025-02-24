namespace CarsStorageApi.Models.UserModels
{
	/// <summary>
	/// Класс данных пользователя, возвращаемых клиенту.
	/// </summary>
	public class UserResponse
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public int Id { get; set; }


        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string? UserName { get; set; }


        /// <summary>
        /// Email пользователя.
        /// </summary>
        public string? Email { get; set; }


        /// <summary>
        /// Список наименования ролей пользователя.
        /// </summary>
        public List<string>? RoleNamesList { get; set; }
    }
}
