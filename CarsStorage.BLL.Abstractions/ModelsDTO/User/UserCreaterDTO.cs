using CarsStorage.BLL.Abstractions.ModelsDTO.Role;

namespace CarsStorage.BLL.Abstractions.ModelsDTO.User
{
	/// <summary>
	/// Класс с данными пользователя (в т.ч. именем, паролем и ролями) для создания пользователя.
	/// </summary>
	public class UserCreaterDTO
	{
		public string? UserName { get; set; }
		public string? Email { get; set; }
		public string? Password { get; set; }
		public List<RoleDTO>? RolesList { get; set; }
	}
}
