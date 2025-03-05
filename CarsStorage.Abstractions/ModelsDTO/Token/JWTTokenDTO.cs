namespace CarsStorage.Abstractions.ModelsDTO.Token
{
	/// <summary>
	/// Класс токена.
	/// </summary>
	public class JWTTokenDTO
	{
		/// <summary>
		/// Строка токена доступа.
		/// </summary>
		public string AccessToken { get; set; }	= string.Empty;


		/// <summary>
		/// Строка токена обновления.
		/// </summary>
		public string RefreshToken { get; set; } = string.Empty;
	}
}
