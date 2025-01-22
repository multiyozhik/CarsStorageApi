using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsStorage.DAL.Entities
{
	/// <summary> 
	/// Сущность машины с свойствами id (автоинкрементация в БД), модель, марка, цвет, количество.
	/// </summary>
	public class CarEntity
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		public string? Model { get; set; }

		[Required]
		public string? Make { get; set; }

		[Required]
		public string? Color { get; set; }

		[Required]
		public int Count { get; set; }
	}
}
