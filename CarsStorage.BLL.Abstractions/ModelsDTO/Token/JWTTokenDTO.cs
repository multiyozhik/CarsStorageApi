namespace CarsStorage.BLL.Abstractions.ModelsDTO.Token
{
	/// <summary>
	/// Класс токена.
	/// </summary>
	public class JWTTokenDTO
	{
		public string AccessToken { get; set; }	= string.Empty;
		public string RefreshToken { get; set; } = string.Empty;
	}
}
