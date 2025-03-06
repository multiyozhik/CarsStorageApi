namespace CarsStorage.Abstractions.ModelsDTO.Location
{
	/// <summary>
	/// Класс возвращаемых данных от DaData API.
	/// </summary>
	public class SuggestionDTO
	{
		/// <summary>
		/// Строка адреса.
		/// </summary>
		public string? value { get; set; }

		/// <summary>
		/// Строка адреса с почтовым индексом.
		/// </summary>
		public string? unrestricted_value { get; set; }

		/// <summary>
		/// Объект адреса с множеством полей.
		/// </summary>
		public AddressDTO? data { get; set; }
	}
}
