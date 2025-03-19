
using CarsStorage.BLL.Services.Config;
using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace CarsStorage.BLL.Services.Kafka
{
	/// <summary>
	/// Класс для отправки сообщения продюсером с заданными конфигурациями.
	/// </summary>
	/// <typeparam name="TMessage">Данные отправляемого сообщения.</typeparam>
	public class KafkaProducer<TMessage> : IKafkaProducer<TMessage>
	{
		/// <summary>
		/// Закрытое поле для продьюсера сообщений.
		/// </summary>
		private readonly IProducer<string, TMessage> producer;

		/// <summary>
		/// Топик сообщений.
		/// </summary>
		private readonly string topic;

		/// <summary>
		/// Конструктор класса для отправки сообщения продюсером с заданными конфигурациями.
		/// </summary>
		/// <param name="kafkaOptions">Объект конфигураций Kafka.</param>
		public KafkaProducer(IOptions<KafkaConfig> kafkaOptions)
        {
			var config = new ProducerConfig { BootstrapServers = kafkaOptions.Value.BootstrapService }; //конфиг. параметры для конфигрирования продююсера

			producer = new ProducerBuilder<string, TMessage>(config)
				.SetValueSerializer(new KafkaJsonSerializer<TMessage>())
				.Build();   //конфигугируем продюсер коллекцией конфиг.параметров, сообщения по ключу типа string, значение типа TMessage и уст. сериализатор для данных сообщения
			topic = kafkaOptions.Value.Topic; //топик - то, куда будем вкладывать наши сообщения
		}


		/// <summary>
		/// Метод для отправки тправки сообщения продюсером в Kafka.
		/// </summary>
		/// <param name="message">Объект отправляемого сообщения.</param>
		/// <param name="cancellationToken">Объект токена отмены.</param>
		public async Task ProduceAsync(TMessage message, CancellationToken cancellationToken)
		{
			var msg = new Message<string, TMessage>()
			{
				Key = "uniq1",
				Value = message
			};
			await producer.ProduceAsync(topic, msg, cancellationToken);
		}

		/// <summary>
		/// Метод для освобождения ресурсов, закрытия подулючения к Kafka.
		/// </summary>
		public void Dispose()
		{
			producer?.Dispose();
		}
	}
}
