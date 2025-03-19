using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace KafkaListenerService.Kafka
{
	/// <summary>
	/// Класс consumer, фоново прослушивающий событие получения собщений от Kafka брокера.
	/// </summary>
	/// <typeparam name="TMessage">Объект получаемого сообщения.</typeparam>
	public class KafkaListener<TMessage> : BackgroundService
    {
        /// <summary>
        /// Закрытое поле топика сообщений.
        /// </summary>
        private readonly string topic;

		/// <summary>
		/// Закрытое поле consumer.
		/// </summary>
		private readonly IConsumer<string, TMessage> consumer;

        /// <summary>
        /// Закрытое поле обработчика при получении сообщения.
        /// </summary>
        private readonly IMessageHandler<TMessage> messageHandler;


		/// <summary>
		/// Конструктор класса consumer, прослушивающего событие получения собщений от Kafka брокера.
		/// </summary>
		/// <param name="kafkaConfig">Объект конфигураций Kafka.</param>
		/// <param name="messageHandler">Объект обработчика при получении сообщения.</param>
		public KafkaListener(IOptions<KafkaConfig> kafkaConfig, IMessageHandler<TMessage> messageHandler)
        {
            var config = new ConsumerConfig()
            {
                BootstrapServers = kafkaConfig.Value.BootstrapService,
                GroupId = kafkaConfig.Value.GroupId
            };
            this.messageHandler = messageHandler;
            topic = kafkaConfig.Value.Topic;
            consumer = new ConsumerBuilder<string, TMessage>(new List<KeyValuePair<string, string>>(config))
                .SetValueDeserializer(new KafkaValueDeserializer<TMessage>())
                .Build();
        }


        /// <summary>
        /// Метод для выполнения фонового прослушивания.
        /// </summary>
        /// <param name="stoppingToken">Объект токена отмены.</param>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(async () => { await ConsumeAsync(stoppingToken); }, stoppingToken);
        }

		/// <summary>
		/// Метод для фонового приема и обработки сообщений.
		/// </summary>
		/// <param name="stoppingToken">Объект токена отмены.</param>
		private async Task ConsumeAsync(CancellationToken stoppingToken)
        {
            consumer.Subscribe(topic);
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var result = consumer.Consume(stoppingToken);
                    await messageHandler.HandleAsync(result.Message.Value, stoppingToken);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

		/// <summary>
		/// Метод для остановки фонового прослушивания приема сообщений.
		/// </summary>
		/// <param name="cancellationToken">Объект токена отмены.</param>
		/// <returns></returns>
		public override Task StopAsync(CancellationToken cancellationToken)
        {
            consumer.Close();
            return base.StopAsync(cancellationToken);
        }
    }
}
