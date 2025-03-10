using System.ComponentModel.DataAnnotations;

namespace CarsStorage.Abstractions.ModelsDTO.Location
{
	/// <summary>
	/// Класс данных о координатах.
	/// </summary>
	public class CoordinateDTO
	{
		/// <summary>
		/// Географическая широта.
		/// </summary>
		public double Lat { get; set; }

		/// <summary>
		/// Географическая долгота.
		/// </summary>
		public double Lon { get; set; }

		/// <summary>
		/// Радиус поиска в метрах.
		/// </summary>
		[Range(1, 1000)]
		public double Radius_meters { get; set; }
	}
}
