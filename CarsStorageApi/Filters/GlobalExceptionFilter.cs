using CarsStorage.BLL.Abstractions.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace CarsStorageApi.Filters
{
	/// <summary>
	/// Класс глобального фильтра исключений.
	/// </summary>
	public class GlobalExceptionFilter : Attribute, IAsyncExceptionFilter
	{		
		public async Task OnExceptionAsync(ExceptionContext context)
		{
			var exception = context.Exception;
			var response = context.HttpContext.Response;

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
