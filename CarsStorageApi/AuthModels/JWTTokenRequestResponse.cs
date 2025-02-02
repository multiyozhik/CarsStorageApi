namespace CarsStorageApi.AuthModels
{
	public class JWTTokenRequestResponse
	{
		public string? AccessToken { get; set; }
		public string? RefreshToken { get; set; }
	}
}
