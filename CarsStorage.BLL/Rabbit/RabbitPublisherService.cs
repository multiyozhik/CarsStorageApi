using CarsStorage.Abstractions.BLL.Services;
using CarsStorage.Abstractions.Exceptions;
using CarsStorage.BLL.Services.Config;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Text;

namespace CarsStorage.BLL.Services.Rabbit
{
	/// <summary>
	/// Класс сервиса для отправки сообщений с помощью RabbitMq.
	/// </summary>
	/// <param name="options">Объект конфигураций RabbiMq.</param>
	/// <param name="logger">Объект для логирования.</param>
	public class RabbitPublisherService(IOptions<RabbitMqConfig> options, ILogger<RabbitPublisherService> logger) : IPublisherService
    {
		/// <summary>
		/// Закрытое поле для конфигураций RabbitMq.
		/// </summary>
		private readonly RabbitMqConfig rabbitMqConfig = options.Value;

		/// <summary>
		/// Метод для отправки сообщений с помощью RabbitMq (рассылка Fanout Exchanger).
		/// </summary>
		/// <param name="message">Строка сообщения.</param>
		public async Task Publish(string message)
        {
			try
			{
				var factory = new ConnectionFactory() { HostName = rabbitMqConfig.HostName, Port = rabbitMqConfig.Port, UserName = rabbitMqConfig.UserName, Password = rabbitMqConfig.Password };
				using var connection = await factory.CreateConnectionAsync();
				using var channel = await connection.CreateChannelAsync();

				await channel.ExchangeDeclareAsync(exchange: rabbitMqConfig.ExchangeName, type: ExchangeType.Fanout, durable: true, autoDelete: false, arguments: null);

				var body = Encoding.UTF8.GetBytes(message);
				await channel.BasicPublishAsync(exchange: rabbitMqConfig.ExchangeName, routingKey: string.Empty, body: body);
				Console.WriteLine($"{DateTime.Now.ToShortTimeString} Отправлено: {message}");
			}
			catch(Exception exception)
			{
				logger.LogError("Ошибка в {service} в {method}  при публикации сообщения с помощью RabbitMq: {errorMessage}", this, nameof(this.Publish), exception.Message);
				throw new ServerException(exception.Message);
			}			
		}
	}
}
