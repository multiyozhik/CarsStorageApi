namespace CarsStorage.Abstractions.ModelsDTO.Location
{
	/// <summary>
	/// Класс метро DaData API.
	/// </summary>
	public class MetroDTO
	{
		/// <summary>
		/// Расстояние до станции метро.
		/// </summary>
		public double? Distance { get; set; }

		/// <summary>
		/// Линия метро.
		/// </summary>
		public string? Line { get; set; }

		/// <summary>
		/// Название станции метро.
		/// </summary>
		public string? Name { get; set; }
	}
}
