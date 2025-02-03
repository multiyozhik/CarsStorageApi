using System.ComponentModel.DataAnnotations;

namespace CarsStorageApi.Models
{
    public class CarRequest
    {
		[Required(ErrorMessage = "Укажите модель автомобиля")]
		public string? Model { get; set; }

		[Required(ErrorMessage = "Укажите марку автомобиля")]
		public string? Make { get; set; }

		[Required(ErrorMessage = "Укажите цвет автомобиля")]
		public string? Color { get; set; }

		[Required(ErrorMessage = "Укажите количество автомобилей")]
		public int Count { get; set; }

		public bool IsAccassible { get; set; }
	}
}
