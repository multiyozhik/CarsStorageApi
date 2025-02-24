namespace CarsStorage.DAL.Entities
{
	/// <summary> 
	/// Класс сущности автомобиля.
	/// </summary>
	public class CarEntity
	{
		/// <summary>
		/// Идентификатор автомобиля.
		/// </summary>
		public int Id { get; set; }


		/// <summary>
		/// Модель автомобиля.
		/// </summary>
		public string? Model { get; set; }


		/// <summary>
		/// Марка автомобиля.
		/// </summary>
		public string? Make { get; set; }


		/// <summary>
		/// Цвет автомобиля.
		/// </summary>
		public string? Color { get; set; }


		/// <summary>
		/// Количество автомобилей.
		/// </summary>
		public int Count { get; set; }


		/// <summary>
		/// Доступен ли автомобиль для просмотра.
		/// </summary>
		public bool? IsAccassible { get; set; }
	}
}
