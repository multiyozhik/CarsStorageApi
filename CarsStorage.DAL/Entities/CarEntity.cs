namespace CarsStorage.DAL.Entities
{
	/// <summary> 
	/// Сущность машины с свойствами id (автоинкрементация в БД), модель, марка, цвет, количество.
	/// </summary>
	public class CarEntity
	{
		public int Id { get; set; }
		public string? Model { get; set; }
		public string? Make { get; set; }
		public string? Color { get; set; }
		public int Count { get; set; }
	}
}
