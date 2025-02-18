namespace CarsStorage.Abstractions.ModelsDTO.User
{
	public class AuthUserData
	{
		public string? UserName { get; set; }
		public string? Email { get; set; }
		public string? AccessTokenFromAuthService { get; set; }
		public List<string> RolesNamesList { get; set; } = [];
	}
}
