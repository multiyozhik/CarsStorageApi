namespace CarsStorageApi.Models.TokenModels
{
    /// <summary>
    /// Класс токена, возвращаемого клиенту или передаваемого от клиента.
    /// </summary>
    public class JWTTokenRequestResponse
    {
        /// <summary>
        /// Строка токена доступа.
        /// </summary>
        public string? AccessToken { get; set; }


        /// <summary>
        /// Строка токена обновления.
        /// </summary>
        public string? RefreshToken { get; set; }
    }
}
