namespace CarsStorage.Abstractions.ModelsDTO.Location
{
	/// <summary>
	/// Класс возвращаемых данных по локации от DaData API.
	/// </summary>
	public class SuggestionDTO
	{
		/// <summary>
		/// Строка адреса.
		/// </summary>
		public string? Value { get; set; }

		/// <summary>
		/// Строка адреса с почтовым индексом.
		/// </summary>
		public string? Unrestricted_value { get; set; }

		/// <summary>
		/// Объект адреса с множеством полей.
		/// </summary>
		public AddressDTO? Data { get; set; }
	}
}
