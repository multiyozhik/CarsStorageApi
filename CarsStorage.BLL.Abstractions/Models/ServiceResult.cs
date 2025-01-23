using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CarsStorage.BLL.Abstractions.Models
{
	public class ServiceResult<T>(T? result, string? errorMessage)
	{
		public T? Result { get; set; } = result;
		public string? ErrorMessage { get; set; } = errorMessage;
		public bool IsSuccess
		{
			get => !string.IsNullOrEmpty(ErrorMessage);
		}
	}
}
