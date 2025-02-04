namespace CarsStorageApi.Models.CarModels
{
	/// <summary>
	/// ����� ��� ������ ���������� � id.
	/// </summary>
	public class CarRequestResponse
    {
        public int Id { get; set; }
        public string? Model { get; set; }
        public string? Make { get; set; }
        public string? Color { get; set; }
        public int Count { get; set; }

        public bool IsAccassible { get; set; }
    }
}
