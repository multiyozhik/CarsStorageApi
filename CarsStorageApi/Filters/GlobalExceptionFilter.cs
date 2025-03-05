using CarsStorage.Abstractions.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace CarsStorageApi.Filters
{
	/// <summary>
	/// Класс глобального фильтра исключений.
	/// </summary>
	/// <param name="logger">Объект для выполнения логирования.</param>
	[AttributeUsage(AttributeTargets.All)]
	public class GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger) : Attribute, IAsyncExceptionFilter
	{		
		/// <summary>
		/// Метод обработки исключения.
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public async Task OnExceptionAsync(ExceptionContext context)
		{

			var exception = context.Exception;
			var response = context.HttpContext.Response;

			var error = exception switch
			{
				BadRequestException badRequestException => $"Некорректный запрос при обработке запроса: {badRequestException.Message}",
				ForbiddenException forbiddenException => $"Запрет доступа к ресурсу при обработке запроса: {forbiddenException.Message}",
				NotFoundException notFoundException => $"Ресурс не найден при обработке запроса: {notFoundException.Message}",
				UnauthorizedAccessException unauthorizedAccessException => $"Ошибка аутентификации при обработке запроса: {unauthorizedAccessException.Message}",
				_ => $"Необработанное исключение: {exception.Message}"
			};
			logger.LogError(error);

			var statusCode = exception switch
			{
				BadRequestException _ => (int)HttpStatusCode.BadRequest,
				ForbiddenException _ => (int)HttpStatusCode.Forbidden,
				NotFoundException _ => (int)HttpStatusCode.NotFound,
				UnauthorizedAccessException _ => (int)HttpStatusCode.Unauthorized,
				_ => (int)HttpStatusCode.InternalServerError
			};
			response.StatusCode = statusCode;
			response.ContentType = "application/json";
			await response.WriteAsJsonAsync(new { ExceptionMessage = exception.Message });
			context.ExceptionHandled = true;
		}
	}
}
