namespace CarsStorage.Abstractions.ModelsDTO.Car
{
	/// <summary>
	/// Класс, представляющий данные для создания нового объекта автомобиля.
	/// </summary>
	public class CarCreaterDTO
	{
		/// <summary>
		/// Модель автомобиля.
		/// </summary>
		public string Model { get; set; } = string.Empty;


		/// <summary>
		/// Марка автомобиля.
		/// </summary>
		public string Make { get; set; } = string.Empty;


		/// <summary>
		/// Цвет автомобиля.
		/// </summary>
		public string Color { get; set; } = string.Empty;


		/// <summary>
		/// Количество автомобилей.
		/// </summary>
		public int Count { get; set; }


		/// <summary>
		/// Доступен ли автомобиль для просмотра.
		/// </summary>
		public bool IsAccassible { get; set; }
	}
}
