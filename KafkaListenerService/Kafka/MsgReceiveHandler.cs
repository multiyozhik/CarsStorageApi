namespace KafkaListenerService.Kafka
{
    /// <summary>
    /// Класс обработчик при получении сообщения от Kafka.
    /// </summary>
    /// <param name="logger">Объект для логирования.</param>
    public class MsgReceiveHandler(ILogger<MsgReceiveHandler> logger) : IMessageHandler<Message>
    {
		/// <summary>
		/// Метод обработки при получении сообщения от Kafka (логирует, что сообщение получено).
		/// </summary>
		/// <param name="message">Объект принимаемого сообщения.</param>
		/// <param name="cancellationToken">Объект токена отмены.</param>
		public Task HandleAsync(Message message, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Сообщение получено: {message.TimeStamp} - {message.Text}");
            return Task.CompletedTask;
        }
    }
}
