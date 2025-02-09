namespace CarsStorageApi.Models.TokenModels
{
    /// <summary>
    /// Класс токена, возвращаемого клиенту или передаваемого от клиента.
    /// </summary>
    public class JWTTokenRequestResponse
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
