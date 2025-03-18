using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ListenerService
{
	/// <summary>
	/// Класс для прослушивания сообщений от RabbitMq в фоновом режиме.
	/// </summary>
	public class Listener : BackgroundService
	{
		/// <summary>
		/// Закрытое поле для конфигураций RabbitMq.
		/// </summary>
		private readonly RabbitMqConfig rabbitMqConfig;

		/// <summary>
		/// Закрытое поле для фабрики создания подключения к RabbitMq с использованием конфигураций.
		/// </summary>
		private readonly ConnectionFactory factory;

		/// <summary>
		/// Объект для логирования.
		/// </summary>
		private readonly ILogger logger;

		/// <summary>
		/// Конструктор для создания объекта для прослушивания сообщений от RabbitMq в фоновом режиме.
		/// </summary>
		/// <param name="options">Объект конфигураций RabbitMq.</param>
		/// <param name="exchangerOptions">Объект конфигураций обменника RabbitMq.</param>
		public Listener(IOptions<RabbitMqConfig> options, ILogger<Listener> logger)
		{
			rabbitMqConfig = options.Value;
			factory = new ConnectionFactory() { HostName = rabbitMqConfig.HostName, Port = rabbitMqConfig.Port, UserName = rabbitMqConfig.UserName, Password = rabbitMqConfig.Password };
			this.logger = logger;
		}


		/// <summary>
		/// Метод фонового режима прослушивания.
		/// </summary>
		/// <param name="stoppingToken">Объект для токена отмены.</param>
		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			try
			{
				using var connection = await factory.CreateConnectionAsync(cancellationToken: stoppingToken);
				using var channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);
				await channel.ExchangeDeclareAsync(exchange: rabbitMqConfig.ExchangeName, ExchangeType.Fanout, durable: true, autoDelete: false, arguments: null, cancellationToken: stoppingToken);

				var queueDeclareResult = await channel.QueueDeclareAsync(cancellationToken: stoppingToken);       //временная очередь
				string queueName = queueDeclareResult.QueueName;
				await channel.QueueBindAsync(queue: queueName, exchange: rabbitMqConfig.ExchangeName, routingKey: string.Empty, cancellationToken: stoppingToken);

				Console.WriteLine("Waiting for messages.");

				var consumer = new AsyncEventingBasicConsumer(channel);
				consumer.ReceivedAsync += async (model, ea) =>
				{
					byte[] body = ea.Body.ToArray();
					var message = Encoding.UTF8.GetString(body);
					Console.WriteLine($"Получено сообщение: {message}");
					await channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false, cancellationToken: stoppingToken);
				};

				await channel.BasicConsumeAsync(queue: queueName, autoAck: false, consumer: consumer, cancellationToken: stoppingToken);

				while (!stoppingToken.IsCancellationRequested)
				{
					await Task.Delay(10000, stoppingToken);                                                     //чтобы сразу не закрывался после запуска
				}
			}
			catch (Exception exception)
			{
				logger.LogError("Ошибка в {service} в {method} при прослушивании сообщений с помощью RabbitMq: {errorMessage}", this, nameof(this.ExecuteAsync), exception.Message);
				throw new Exception(exception.Message);
			}
		}
	}
}
