namespace CarsStorage.BLL.Abstractions.ModelsDTO.CarDTO
{
	public class CarCreaterDTO
	{
		public string? Model { get; set; }
		public string? Make { get; set; }
		public string? Color { get; set; }
		public int Count { get; set; }
		public bool? IsAccassible { get; set; }
	}
}
