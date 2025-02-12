namespace CarsStorageApi.Models.CarModels
{
	/// <summary>
	/// ����� ������ ����������, ������������ �������.
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
