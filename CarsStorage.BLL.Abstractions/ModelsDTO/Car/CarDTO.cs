namespace CarsStorage.BLL.Abstractions.ModelsDTO.Car
{
	/// <summary>
	/// Класс с данными об автомобиле с id.
	/// </summary>
	public class CarDTO
	{
		public int Id { get; set; }
		public string? Model { get; set; }
		public string? Make { get; set; }
		public string? Color { get; set; }
		public int Count { get; set; }
		public bool? IsAccassible { get; set; }
	}
}
