namespace CarsStorage.BLL.Abstractions.ModelsDTO.CarDTO
{
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
