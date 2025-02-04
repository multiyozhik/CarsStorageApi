namespace CarsStorage.BLL.Abstractions.ModelsDTO.AuthModels
{
	/// <summary>
	/// Класс токена.
	/// </summary>
	public class JWTTokenDTO
	{
		public string? AccessToken { get; set; }
		public string? RefreshToken { get; set; }
	}
}
