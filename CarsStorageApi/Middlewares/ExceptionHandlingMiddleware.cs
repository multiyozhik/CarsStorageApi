using CarsStorage.Abstractions.Exceptions;

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
				if (httpContext.Response.StatusCode == StatusCodes.Status401Unauthorized)
					throw new UnauthorizedAccessException($"Попытка неавторизованного доступа при обработке запроса: {httpContext.Request.Path}");
				if (httpContext.Response.StatusCode == StatusCodes.Status403Forbidden)
					throw new ForbiddenException($"Доступ запрещен при обработке запроса: {httpContext.Request.Path}");
			}
			catch (Exception exception)
			{
				var error = exception switch
				{
					BadRequestException _ => $"Некорректный запрос.",
					ForbiddenException _ => $"Ошибка авторизации.",
					NotFoundException _ => $"Ресурс не найден.",
					UnauthorizedAccessException _ => $"Ошибка аутентификации.",
					_ => $"Необработанное исключение."
				};
				logger.LogError("Статус код {status}. {error} {message}", httpContext.Response.StatusCode, error, exception.Message);
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
			if (response.StatusCode == StatusCodes.Status401Unauthorized || response.StatusCode == StatusCodes.Status403Forbidden)
				await httpContext.Response.WriteAsync(exception.Message);
			else
			{
				response.ContentType = "application/json";
				await response.WriteAsJsonAsync(new { ExceptionMessage = exception.Message, StatusCodes = response.StatusCode });
			}
		}
	}
}
