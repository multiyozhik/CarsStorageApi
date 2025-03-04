using CarsStorage.Abstractions.ModelsDTO.Role;

namespace CarsStorage.BLL.Abstractions.ModelsDTO.User
{
	/// <summary>
	/// Класс пользователя, включая его идентификатор.
	/// </summary>
	public class UserDTO
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
		public string Email { get; set; } =	string.Empty;


		/// <summary>
		/// Список объектов ролей пользователя.
		/// </summary>
		public List<RoleDTO>? RolesList { get; set; }
	}
}
