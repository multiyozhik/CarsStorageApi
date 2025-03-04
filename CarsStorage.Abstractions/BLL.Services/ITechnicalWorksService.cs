using CarsStorage.Abstractions.General;

namespace CarsStorage.Abstractions.BLL.Services
{
	/// <summary>
	/// Интерфейс для сервиса по проведению технических работ.
	/// </summary>
	public interface ITechnicalWorksService
	{
		/// <summary>
		/// Метод для запуска технических работ.
		/// </summary>
		/// <returns>Строка сообщения о запуске технических работ.</returns>
		public Task<ServiceResult<string>> StartTechnicalWorks();


		/// <summary>
		/// Метод для прекращения технических работ.
		/// </summary>
		/// <returns>Строка сообщения о прекращении технических работ.</returns>
		public Task<ServiceResult<string>> StopTechnicalWorks();


		/// <summary>
		/// Метод возвращает булево значение, проводятся ли технические работы в настоящий момент.
		/// </summary>
		/// <returns>Возвращает true, если проводятся технические работы.</returns>
		public Task<ServiceResult<bool>> IsUnderMaintenance();
	}
}
