using System.ComponentModel.DataAnnotations;

namespace CarsStorageApi.Models.CarModels
{
    /// <summary>
    /// Класс данных автомобиля, передаваемых клиентом.
    /// </summary>
    public class CarRequest
    {
        [Required(ErrorMessage = "Укажите модель автомобиля")]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки д.б. от 3 до 30 символов")]
		public string Model { get; set; } = string.Empty;

		[Required(ErrorMessage = "Укажите марку автомобиля")]
		[StringLength(50, MinimumLength = 2, ErrorMessage = "Длина строки д.б. от 3 до 30 символов")]
		public string Make { get; set; } = string.Empty;

		[Required(ErrorMessage = "Укажите цвет автомобиля")]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки д.б. от 3 до 30 символов")]
		public string Color { get; set; } = string.Empty;

		[Required(ErrorMessage = "Укажите количество автомобилей")]
        public int Count { get; set; }
        public bool IsAccassible { get; set; }
    }
}
