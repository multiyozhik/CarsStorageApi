namespace CarsStorage.Abstractions.ModelsDTO.User
{
	public class AuthUserDataDTO
	{
		public string? UserName { get; set; }
		public string? Email { get; set; }
		public List<string> RolesNamesList { get; set; } = [];
	}
}
