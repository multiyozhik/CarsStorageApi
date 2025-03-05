using System.ComponentModel.DataAnnotations;

namespace CarsStorageApi.Models.CarModels
{
    /// <summary>
    /// Класс данных автомобиля, передаваемых клиентом.
    /// </summary>
    public class CarRequest
    {
		/// <summary>
		/// Модель автомобиля.
		/// </summary>
        [Required(ErrorMessage = "Укажите модель автомобиля")]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки д.б. от 3 до 30 символов")]
		public string Model { get; set; } = string.Empty;


		/// <summary>
		/// Марка автомобиля.
		/// </summary>
		[Required(ErrorMessage = "Укажите марку автомобиля")]
		[StringLength(50, MinimumLength = 2, ErrorMessage = "Длина строки д.б. от 3 до 30 символов")]
		public string Make { get; set; } = string.Empty;


		/// <summary>
		/// Цвет автомобиля.
		/// </summary>
		[Required(ErrorMessage = "Укажите цвет автомобиля")]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки д.б. от 3 до 30 символов")]
		public string Color { get; set; } = string.Empty;


		/// <summary>
		/// Количество автомобилей.
		/// </summary>
		[Required(ErrorMessage = "Укажите количество автомобилей")]
        public int Count { get; set; }


		/// <summary>
		/// Доступен ли автомобиль для просмотра.
		/// </summary>
		public bool IsAccassible { get; set; }
    }
}
