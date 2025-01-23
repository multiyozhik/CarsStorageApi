using System.ComponentModel.DataAnnotations;

namespace CarsStorageApi.Models
{
	public class CarCountChangerRequest
	{
		[Required]
		public int Id { get; set; }
		
		[Required]
		public int Count { get; set; }
	}
}
