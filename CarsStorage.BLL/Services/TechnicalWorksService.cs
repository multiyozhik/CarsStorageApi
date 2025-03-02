using CarsStorage.Abstractions.BLL.Services;

namespace CarsStorage.BLL.Services.Services
{
	/// <summary>
	/// Класс сервиса по проведению технических работ.
	/// </summary>
	/// <param name="repository">Репозиторий технических работ.</param>
	public class TechnicalWorksService(ITechnicalWorksRepository repository) : ITechnicalWorksService
	{
		/// <summary>
		/// Метод возвращает булево значение, проводятся ли технические работы.
		/// </summary>
		/// <returns>Возвращает true, если проводятся технические работы.</returns>
		public async Task<bool> HasTechnicalWorks()
		{
			return await dbContext.TechnicalWorks.AnyAsync(w => w.StartTime <= DateTime.Now && w.EndTime >= DateTime.Now);
			return false;
		} 
	}
}
