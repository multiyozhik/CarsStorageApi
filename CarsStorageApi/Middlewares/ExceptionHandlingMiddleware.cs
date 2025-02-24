using CarsStorage.Abstractions.Exceptions;
using System.Net;

namespace CarsStorageApi.Middlewares
{
	/// <summary>
	/// Класс middleware для обработки исключений.
	/// </summary>
	/// <param name="next">Делегат для обработки http-запроса.</param>
	public class ExceptionHandlingMiddleware(RequestDelegate next)
	{
		/// <summary>
		/// Метод обработки http-запроса.
		/// </summary>
		/// <param name="httpContext"></param>
		/// <returns></returns>
		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await next(httpContext);
			}
			catch (Exception exception)
			{
				await HandleExceptionAsync(httpContext, exception);
			}
		}


		/// <summary>
		/// Метод обработки исключения, возвращает объект, содержащий статус код ошибки и ее сообщение.
		/// </summary>
		/// <param name="httpContext">Объект http-контекста.</param>
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
