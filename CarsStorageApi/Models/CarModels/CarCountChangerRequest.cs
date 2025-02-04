using System.ComponentModel.DataAnnotations;

namespace CarsStorageApi.Models.CarModels
{
    public class CarCountChangerRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int Count { get; set; }
    }
}
