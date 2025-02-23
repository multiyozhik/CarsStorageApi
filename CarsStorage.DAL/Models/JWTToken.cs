namespace CarsStorage.DAL.Models
{
	/// <summary>
	/// Класс токена.
	/// </summary>
	public class JWTToken
	{
		public string AccessToken { get; set; } = string.Empty;
		public string RefreshToken { get; set; } = string.Empty;
	}
}
