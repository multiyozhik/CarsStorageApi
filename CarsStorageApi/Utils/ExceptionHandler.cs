using CarsStorage.BLL.Abstractions.Exceptions;
using Microsoft.AspNetCore.Mvc;


namespace CarsStorageApi.Utils
{
	/// <summary>
	/// Класс статический для преобразования объектов ошибок из слоя сервисов в ActionResult для возможности возврата контроллером.
	/// </summary>
	public static class ExceptionHandler
	{
        public static ActionResult HandleException(ControllerBase controller, Exception exception) 
        {
			return exception switch
			{
				BadRequestException _ => controller.BadRequest(exception.Message),
				ForbiddenException _ => controller.Forbid(exception.Message),
				NotFoundException _ => controller.NotFound(exception.Message),
				UnauthorizedException _ => controller.Unauthorized(exception.Message),
				_ => new StatusCodeResult(500)
			};
		}
    }
}
