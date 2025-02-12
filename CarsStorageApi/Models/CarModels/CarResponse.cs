namespace CarsStorageApi.Models.CarModels
{
	/// <summary>
	///  ласс данных автомобил€, возвращаемых клиенту.
	/// </summary>
	public class CarResponse
    {
        public int Id { get; set; }
        public string Model { get; set; } = string.Empty;
        public string Make { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public int Count { get; set; }
        public bool IsAccassible { get; set; }
    }
}
