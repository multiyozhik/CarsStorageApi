namespace CarsStorage.Abstractions.ModelsDTO.Location
{
    /// <summary>
    /// Класс данных о локации пользователя.
    /// </summary>
    public class LocationDTO
    {
        /// <summary>
        /// Список адресов.
        /// </summary>
        public List<string> AddressList { get; set; } = [];
    }
}
