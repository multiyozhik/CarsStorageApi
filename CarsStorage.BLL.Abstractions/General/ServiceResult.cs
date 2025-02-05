using Microsoft.AspNetCore.Mvc;

namespace CarsStorage.BLL.Abstractions.Models
{
	/// <summary>
	/// Класс результата работы сервиса.
	/// </summary>
	public class ServiceResult<T>(T? result, Exception? serviceError)
	{
		public T? Result { get; set; } = result;
		public Exception? ServiceError { get; set; } = serviceError;
		public bool IsSuccess
		{
			get => ServiceError is null && Result is not null;
		}
	}
}
