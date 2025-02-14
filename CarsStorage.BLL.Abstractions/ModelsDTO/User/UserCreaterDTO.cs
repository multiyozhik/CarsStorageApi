
namespace CarsStorage.Abstractions.ModelsDTO.User
{
	/// <summary>
	/// Класс с данными пользователя (в т.ч. именем, паролем и ролями) для создания пользователя.
	/// </summary>
	public class UserCreaterDTO
	{
		public string UserName { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public List<string> RoleNamesList { get; set; } = [];
	}
}
