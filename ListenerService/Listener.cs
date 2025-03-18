using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ListenerService
{
	/// <summary>
	/// ����� ��� ������������� ��������� �� RabbitMq � ������� ������.
	/// </summary>
	public class Listener : BackgroundService
	{
		/// <summary>
		/// �������� ���� ��� ������������ RabbitMq.
		/// </summary>
		private readonly RabbitMqConfig rabbitMqConfig;

		/// <summary>
		/// �������� ���� ��� ������� �������� ����������� � RabbitMq � �������������� ������������.
		/// </summary>
		private readonly ConnectionFactory factory;

		/// <summary>
		/// ������ ��� �����������.
		/// </summary>
		private readonly ILogger logger;

		/// <summary>
		/// ����������� ��� �������� ������� ��� ������������� ��������� �� RabbitMq � ������� ������.
		/// </summary>
		/// <param name="options">������ ������������ RabbitMq.</param>
		/// <param name="exchangerOptions">������ ������������ ��������� RabbitMq.</param>
		public Listener(IOptions<RabbitMqConfig> options, ILogger<Listener> logger)
		{
			rabbitMqConfig = options.Value;
			factory = new ConnectionFactory() { HostName = rabbitMqConfig.HostName, Port = rabbitMqConfig.Port, UserName = rabbitMqConfig.UserName, Password = rabbitMqConfig.Password };
			this.logger = logger;
		}


		/// <summary>
		/// ����� �������� ������ �������������.
		/// </summary>
		/// <param name="stoppingToken">������ ��� ������ ������.</param>
		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			try
			{
				using var connection = await factory.CreateConnectionAsync(cancellationToken: stoppingToken);
				using var channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);
				await channel.ExchangeDeclareAsync(exchange: rabbitMqConfig.ExchangeName, ExchangeType.Fanout, durable: true, autoDelete: false, arguments: null, cancellationToken: stoppingToken);

				var queueDeclareResult = await channel.QueueDeclareAsync(cancellationToken: stoppingToken);       //��������� �������
				string queueName = queueDeclareResult.QueueName;
				await channel.QueueBindAsync(queue: queueName, exchange: rabbitMqConfig.ExchangeName, routingKey: string.Empty, cancellationToken: stoppingToken);

				Console.WriteLine("Waiting for messages.");

				var consumer = new AsyncEventingBasicConsumer(channel);
				consumer.ReceivedAsync += async (model, ea) =>
				{
					byte[] body = ea.Body.ToArray();
					var message = Encoding.UTF8.GetString(body);
					Console.WriteLine($"�������� ���������: {message}");
					await channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false, cancellationToken: stoppingToken);
				};

				await channel.BasicConsumeAsync(queue: queueName, autoAck: false, consumer: consumer, cancellationToken: stoppingToken);

				while (!stoppingToken.IsCancellationRequested)
				{
					await Task.Delay(10000, stoppingToken);                                                     //����� ����� �� ���������� ����� �������
				}
			}
			catch (Exception exception)
			{
				logger.LogError("������ � {service} � {method} ��� ������������� ��������� � ������� RabbitMq: {errorMessage}", this, nameof(this.ExecuteAsync), exception.Message);
				throw new Exception(exception.Message);
			}
		}
	}
}
