using System.ComponentModel.DataAnnotations;

namespace CarsStorageApi.Models.CarModels
{
	/// <summary>
	/// Класс для передачи клиентом нового значения количества автомобилей с id автомобиля.
	/// </summary>
	public class CarCountRequest
    {
        /// <summary>
        /// Идентификатор автомобиля.
        /// </summary>
        [Required(ErrorMessage = "Укажите Id автомобиля")]
        public int Id { get; set; }

        /// <summary>
        /// Количество автомобилей.
        /// </summary>
        [Required(ErrorMessage = "Укажите количество автомобилей")]
        public int Count { get; set; }
    }
}
