using System.ComponentModel.DataAnnotations;

namespace CarsStorageApi.Models.CarModels
{
	/// <summary>
	/// Класс для передачи клиентом нового значения количества автомобилей с id автомобиля.
	/// </summary>
	public class CarCountRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int Count { get; set; }
    }
}
