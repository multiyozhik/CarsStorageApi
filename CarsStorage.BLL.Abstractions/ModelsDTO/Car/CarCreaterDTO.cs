namespace CarsStorage.BLL.Abstractions.ModelsDTO.Car
{
	/// <summary>
	/// Класс с данными от пользователя для создания новой записи об автомобиле.
	/// </summary>
	public class CarCreaterDTO
	{
		public string? Model { get; set; }
		public string? Make { get; set; }
		public string? Color { get; set; }
		public int Count { get; set; }
		public bool? IsAccassible { get; set; }
	}
}
