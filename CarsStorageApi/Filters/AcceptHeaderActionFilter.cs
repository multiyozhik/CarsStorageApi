using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CarsStorageApi.Filters
{
	/// <summary>
	/// Класс фильтра действия для того, чтобы не пропускать запросы при отсутствии Accept заголовка в запросе.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class AcceptHeaderActionFilter : Attribute, IAsyncActionFilter
	{
		/// <summary>
		/// Метод фильтра для проверки наличия Accept заголовка в запросе.
		/// </summary>
		/// <param name="context">Контекст фильтра.</param>
		/// <param name="next">Следующий делегат в конвейере обработки запроса.</param>
		/// <returns>Возвращает 406 NotAcceptable ошибку при отсутствии Accept заголовка в запросе.</returns>
		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			if (!context.HttpContext.Request.Headers.ContainsKey("Accept"))
			{
				context.Result = new ContentResult
				{
					StatusCode = StatusCodes.Status406NotAcceptable, 
					Content = "Accept заголовок не установлен в запросе."
				};
			}
			await next();
		}
	}
}
