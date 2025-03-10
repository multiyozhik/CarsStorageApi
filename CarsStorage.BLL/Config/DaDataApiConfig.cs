namespace CarsStorage.BLL.Services.Config
{
	/// <summary>
	/// Конфигурации для DaData API.
	/// </summary>
	public class DaDataApiConfig
	{
		/// <summary>
		/// Сторока Url для API: обратное геокодирование.
		/// </summary>
		public string? LocationApiUrl { get; set; }

		/// <summary>
		/// Токен для доступа к API.
		/// </summary>
		public string? Token { get; set; }
	}
}
