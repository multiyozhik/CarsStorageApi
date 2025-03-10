using System.ComponentModel.DataAnnotations;

namespace CarsStorageApi.Models.LocationModels
{
	/// <summary>
	/// Класс данных о передаваемых координатах.
	/// </summary>
	public class CoordinateRequest
    {
		/// <summary>
		/// Географическая широта.
		/// </summary>
		[Required(ErrorMessage = "Укажите географическую широту.")]
		public double lat {  get; set; }

		/// <summary>
		/// Географическая долгота.
		/// </summary>
		[Required(ErrorMessage = "Укажите географическую долготу.")]
		public double lon { get; set; }

		/// <summary>
		/// Радиус поиска в метрах.
		/// </summary>
		[Required(ErrorMessage = "Укажите радиус поиска в метрах.")]
		[Range(1, 1000)]
		public double radius_meters { get; set; } = 100;
	}
}
