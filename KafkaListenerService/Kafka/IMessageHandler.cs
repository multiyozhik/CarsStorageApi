namespace KafkaListenerService.Kafka
{
    /// <summary>
    /// Обобщенный интерфейс обработчика сообщений от Kafka.
    /// </summary>
    /// <typeparam name="TMessage">Обобщенный тип сообщений, получаемых от Kafka.</typeparam>
    public interface IMessageHandler<TMessage>
    {
        /// <summary>
        /// Метод обработки получаемых сообщений от Kafka.
        /// </summary>
        /// <param name="message">Обхект получаемого сообщения типа IMessage.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        public Task HandleAsync(TMessage message, CancellationToken cancellationToken);
    }
}
