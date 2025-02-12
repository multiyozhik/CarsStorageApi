namespace CarsStorageApi.Models.UserModels
{
	/// <summary>
	/// Класс данных пользователя, возвращаемых клиенту.
	/// </summary>
	public class UserResponse
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public List<string>? RoleNamesList { get; set; }
    }
}
