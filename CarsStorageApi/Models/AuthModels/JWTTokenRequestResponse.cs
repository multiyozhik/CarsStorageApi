namespace CarsStorageApi.Models.AuthModels
{
    /// <summary>
    /// Класс токена.
    /// </summary>
    public class JWTTokenRequestResponse
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
