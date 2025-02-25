using CarsStorage.Abstractions.Exceptions;
using System.Net;

namespace CarsStorageApi.Middlewares
{
	/// <summary>
	/// Класс middleware для обработки исключений.
	/// </summary>
	/// <param name="next">Делегат для обработки http-запроса.</param>
	/// <param name="logger">Объект для выполнения логирования.</param>
	public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
	{
		/// <summary>
		/// Метод обработки http-запроса.
		/// </summary>
		/// <param name="httpContext">Контекст http-запроса.</param>
		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await next(httpContext);
			}
			catch (Exception exception)
			{
				var error = exception switch
				{
					BadRequestException badRequestException => $"Некорректный запрос: {badRequestException.Message}",
					ForbiddenException forbiddenException => $"Запрет доступа к ресурсу при обработке запроса: {forbiddenException.Message}",
					NotFoundException notFoundException => $"Ресурс не найден при обработке запроса: {notFoundException.Message}",
					UnauthorizedAccessException unauthorizedAccessException => $"Ошибка аутентификации при обработке запроса: {unauthorizedAccessException.Message}", 
					_ => $"Необработанное исключение: {exception.Message}"
				};
				logger.LogError(error);
				await HandleExceptionAsync(httpContext, exception);
			}
		}


		/// <summary>
		/// Метод обработки исключения, возвращает в ответе клиенту статус код ошибки и ее сообщение.
		/// </summary>
		/// <param name="httpContext">Контекст http-запроса.</param>
		/// <param name="exception">Объект исключения.</param>
		private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
		{
			var response = httpContext.Response;
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
		}
	}
}
