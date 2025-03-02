namespace CarsStorage.Abstractions.BLL.Services
{
	/// <summary>
	/// Интерфейс для сервиса по проведению технических работ.
	/// </summary>
	public interface ITechnicalWorksService
	{
		/// <summary>
		/// Метод возвращает булево значение, проводятся ли технические работы.
		/// </summary>
		/// <returns>Возвращает true, если проводятся технические работы.</returns>
		public Task<bool> HasTechnicalWorks();
	}
}
