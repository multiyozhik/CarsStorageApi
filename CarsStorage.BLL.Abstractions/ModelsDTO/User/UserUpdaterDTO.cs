namespace CarsStorage.BLL.Abstractions.ModelsDTO.User
{
	public class UserUpdaterDTO
	{
		public int Id { get; set; }
		public string UserName { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public List<string>? RoleNamesList { get; set; } = [];
	}
}
