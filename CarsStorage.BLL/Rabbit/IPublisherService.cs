namespace CarsStorage.Abstractions.BLL.Services
{
	/// <summary>
	/// Интерфейс для сервиса отправки сообщений с помощью брокера сообщений.
	/// </summary>
	public interface IPublisherService
	{
		/// <summary>
		/// Метод для отправки сообщений с помощью брокера сообщений.
		/// </summary>
		/// <param name="message">Строка сообщения.</param>
		public Task Publish(string message);
	}
}
