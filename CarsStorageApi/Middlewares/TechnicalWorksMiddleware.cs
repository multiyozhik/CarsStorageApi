using CarsStorage.Abstractions.BLL.Services;

namespace CarsStorageApi.Middlewares
{
	/// <summary>
	/// Класс middleware для обработки запроса при проведении технических работ.
	/// </summary>
	/// <param name="next">Следующий делегат конвейера обработки запроса.</param>
	/// <param name="technicalWorksService">Сервис по проведению технических работ.</param>
	public class TechnicalWorksMiddleware(RequestDelegate next, ITechnicalWorksService technicalWorksService)
	{
		/// <summary>
		/// Метод обработки http-запроса.
		/// </summary>
		/// <param name="context">Контекст http-запроса.</param>
		/// <returns>Возвращает 503 ошибку при проведении технических работ.</returns>
		public async Task Invoke(HttpContext context)
		{
			var isUnderMaintenance =  await technicalWorksService.IsUnderMaintenance();  //try-catch
			if (isUnderMaintenance.Result)
			{
				context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
				await context.Response.WriteAsync("Сервис временно недоступен, ведутся технические работы по обслуживанию.");
				return;
			}
			await next(context); 
		}
	}
}
