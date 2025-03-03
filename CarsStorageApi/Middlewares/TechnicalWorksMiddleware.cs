using CarsStorage.Abstractions.BLL.Services;

namespace CarsStorageApi.Middlewares
{
	/// <summary>
	/// Класс middleware для обработки запроса при проведении технических работ.
	/// </summary>
	/// <param name="next">Следующий делегат конвейера обработки запроса.</param>
	/// <param name="technicalWorksService">Сервис по проведению технических работ.</param>
	public class TechnicalWorksMiddleware(ITechnicalWorksService technicalWorksService): IMiddleware
	{
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			var isUnderMaintenance = await technicalWorksService.IsUnderMaintenance();  //try-catch
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
