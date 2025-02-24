namespace CarsStorageApi.Models.CarModels
{
	/// <summary>
	/// Класс данных автомобиля, возвращаемых клиенту.
	/// </summary>
	public class CarResponse
    {
        /// <summary>
        /// Индентификатор объекта автомобиля.
        /// </summary>
        public int Id { get; set; }

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
