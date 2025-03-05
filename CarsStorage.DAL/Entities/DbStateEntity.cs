namespace CarsStorage.DAL.Entities
{
	/// <summary>
	/// Сущность состояния (конфигурации) БД.
	/// </summary>
	public class DbStateEntity
	{
		/// <summary>
		/// Идентификатор для состояния БД.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Наименование параметра состояния БД .
		/// </summary>
		public string StateName { get; set; } = string.Empty;

		/// <summary>
		/// Значение параметра состояния БД.
		/// </summary>
		public bool Value { get; set; }
	}
}
