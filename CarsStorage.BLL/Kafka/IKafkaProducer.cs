namespace CarsStorage.BLL.Services.Kafka
{
	/// <summary>
	/// Интерфейс для публикации сообщения в Kafka.
	/// </summary>
	/// <typeparam name="TMessage">Объект отправляемого сообщения.</typeparam>
	public interface IKafkaProducer<in TMessage>: IDisposable
	{
		/// <summary>
		/// Метод для публикации сообщения в Kafka.
		/// </summary>
		/// <param name="message">Объект отправляемого сообщения.</param>
		/// <param name="cancellationToken">Объект токена отмены.</param>
		/// <returns></returns>
		public Task ProduceAsync(TMessage message, CancellationToken cancellationToken);
	}
}
