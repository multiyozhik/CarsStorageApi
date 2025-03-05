using CarsStorage.Abstractions.BLL.Services;
using CarsStorageApi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarsStorageApi.Controllers
{
	/// <summary>
	/// Класс контроллера для технических работ.
	/// </summary>
	/// <param name="worksService">Объект сервиса для технических работ.</param>
	/// <param name="logger">Объект для логирования.</param>
	[ApiController]
	[Authorize(Policy = "RequierManageUsers")]
	[Route("[controller]/[action]")]
	[ServiceFilter(typeof(AcceptHeaderActionFilter))]
	public class TechnicalWorksController(ITechnicalWorksService worksService, ILogger<TechnicalWorksController> logger) : ControllerBase
	{
		/// <summary>
		/// Метод для запуска технических работ.
		/// </summary>
		/// <returns>Строка сообщения о начатых технических работах.</returns>
		[HttpGet]
		public async Task<ActionResult<string>> StartTechnicalWorks()
		{
			try
			{
				var worksServiceResult = await worksService.StartTechnicalWorks();
				if (!worksServiceResult.IsSuccess)				
					throw worksServiceResult.ServiceError;
				return worksServiceResult.Result;
			}
			catch (Exception exception)
			{
				logger.LogError("Ошибка в {controller} в методе {method} при запуске технических работ: {errorMessage}", this, nameof(this.StartTechnicalWorks), exception.Message);
				throw;
			}
		}


		/// <summary>
		/// Метод для прекращения технических работ.
		/// </summary>
		/// <returns>Строка сообщения о завершении технических работ.</returns>
		[HttpGet]
		public async Task<ActionResult<string>> StopTechnicalWorks()
		{
			try
			{
				var worksServiceResult = await worksService.StopTechnicalWorks();
				if (!worksServiceResult.IsSuccess)
					throw worksServiceResult.ServiceError;
				return worksServiceResult.Result;
			}
			catch (Exception exception)
			{
				logger.LogError("Ошибка в {controller} в методе {method} при прекращении технических работ: {errorMessage}", this, nameof(this.StopTechnicalWorks), exception.Message);
				throw;
			}
		}
	}
}
