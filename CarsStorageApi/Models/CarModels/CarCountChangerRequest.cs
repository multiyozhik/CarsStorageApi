using System.ComponentModel.DataAnnotations;

namespace CarsStorageApi.Models.CarModels
{
    /// <summary>
    /// Класс с id автомобиля и новым значением количества автомобилей.
    /// </summary>
    public class CarCountChangerRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int Count { get; set; }
    }
}
