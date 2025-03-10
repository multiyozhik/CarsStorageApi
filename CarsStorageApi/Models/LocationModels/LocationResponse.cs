namespace CarsStorageApi.Models.LocationModels
{
	/// <summary>
	/// Класс данных о локации пользователя, возвращаемых клиенту.
	/// </summary>
	public class LocationResponse
	{
		/// <summary>
		/// Список адресов.
		/// </summary>
		public List<string> AddressList { get; set; } = [];
	}
}
