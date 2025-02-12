namespace CarsStorage.BLL.Abstractions.General
{
	/// <summary>
	/// Типизированный класс результата работы сервис (конструктор - для результата и конструктор - для ошибки). 
	/// </summary>
	public class ServiceResult<T>
	{
		private readonly T? result;
		private readonly Exception? serviceError;
		public T Result => result ?? throw new InvalidOperationException("Результат сервиса не установлен.");
		public Exception ServiceError => serviceError ?? throw new InvalidOperationException("Ошибка сервиса не установлена.");
		public bool IsSuccess => result is not null;   //результат сервиса в дальнейшем в методах будем возвращать через проверку

		public ServiceResult(T result)
		{
			this.result = result;
			serviceError = null;
		}

		public ServiceResult(Exception serviceError)
		{
			this.serviceError = serviceError;
			result = default;
		}
	}
}
