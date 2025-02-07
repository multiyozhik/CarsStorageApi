﻿using CarsStorage.BLL.Abstractions.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace CarsStorageApi.Middlewares
{
	/// <summary>
	/// middleware для обработки исключений.
	/// </summary>
	/// <param name="next"></param>
	public class ExceptionHandlingMiddleware(RequestDelegate next)
	{
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

		private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
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
