
namespace CarsStorage.DAL.Entities
{
	public class CarRow
	{
		public Guid Id { get; set; }
		public string? Model { get; set; }
		public string? Make { get; set; }
		public string? Color { get; set; }
		public int Count { get; set; }
	}
}
