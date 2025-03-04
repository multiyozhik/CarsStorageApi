namespace CarsStorage.Abstractions.General
{
	/// <summary>
	/// Типизированный класс, представляющий результат работы сервиса. 
	/// </summary>
	public class ServiceResult<T>
	{
		/// <summary>
		/// Результат сервиса.
		/// </summary>
		private readonly T? result;

		/// <summary>
		/// Исключение, возникающего при работе сервиса.
		/// </summary>
		private readonly Exception? serviceError;
	
		/// <summary>
		/// Свойство, представляющее результат сервиса.
		/// </summary>
		public T Result => result ?? throw new InvalidOperationException("Результат сервиса не установлен.");

		/// <summary>
		/// Свойство исключения, возникающего при работе сервиса.
		/// </summary>
		public Exception ServiceError => serviceError ?? throw new InvalidOperationException("Ошибка сервиса не установлена.");

		/// <summary>
		/// Свойство, возвращающее булево значение, получен ли результат сервиса.
		/// </summary>
		public bool IsSuccess => result is not null; 


		/// <summary>
		/// Конструктор для инициализации объекта результата сервиса, возвращающего его результат.
		/// </summary>
		/// <param name="result">Результат сервиса.</param>
		public ServiceResult(T result)
		{
			this.result = result;
			serviceError = null;
		}


		/// <summary>
		/// Конструктор для инициализации объекта результата сервиса, возвращающего исключение.
		/// </summary>
		/// <param name="serviceError">Исключение, возникающее при работе сервиса.</param>
		public ServiceResult(Exception serviceError)
		{
			this.serviceError = serviceError;
			result = default;
		}
	}
}
