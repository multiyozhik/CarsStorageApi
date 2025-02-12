namespace CarsStorage.BLL.Abstractions.ModelsDTO.Car
{
	/// <summary>
	/// Класс с данными от пользователя для создания новой записи об автомобиле.
	/// </summary>
	public class CarCreaterDTO
	{
		public string Model { get; set; } = string.Empty;
		public string Make { get; set; } = string.Empty;
		public string Color { get; set; } = string.Empty;
		public int Count { get; set; }
		public bool IsAccassible { get; set; }
	}
}
