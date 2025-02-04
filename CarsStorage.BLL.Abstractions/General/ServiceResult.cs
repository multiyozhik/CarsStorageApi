namespace CarsStorage.BLL.Abstractions.Models
{
	/// <summary>
	/// Класс результата работы сервиса.
	/// </summary>
	public class ServiceResult<T>(T? result, string? errorMessage)
	{
		public T? Result { get; set; } = result;
		public string? ErrorMessage { get; set; } = errorMessage;
		public bool IsSuccess
		{
			get => !string.IsNullOrEmpty(ErrorMessage) && Result is not null;
		}
	}
}
