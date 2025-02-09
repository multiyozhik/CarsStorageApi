using CarsStorage.BLL.Abstractions.ModelsDTO.Role;

namespace CarsStorage.BLL.Abstractions.ModelsDTO.User
{
	/// <summary>
	/// Класс пользователя с id, именем и ролями.
	/// </summary>
	public class UserDTO
	{
		public int Id { get; set; }
		public string? UserName { get; set; }
		public string? Email { get; set; }
		public List<RoleDTO>? RolesList { get; set; }
	}
}
