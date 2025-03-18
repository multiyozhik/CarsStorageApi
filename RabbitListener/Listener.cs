using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitListener
{
	public class Listener : IListener
	{
		public async Task Recieve(string message)
		{
			var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672, UserName = "multiyozhik", Password = "sviri_30251721" };
			using var connection = await factory.CreateConnectionAsync();
			using var channel = await connection.CreateChannelAsync();

			await channel.ExchangeDeclareAsync(exchange: "techWorks", type: "Fanout", durable: true, autoDelete: false, arguments: null);

			QueueDeclareOk queueDeclareResult = await channel.QueueDeclareAsync();
			string queueName = queueDeclareResult.QueueName;                      
			await channel.QueueBindAsync(queue: queueName, exchange: "techWorks", routingKey: string.Empty);

			Console.WriteLine("Waiting for messages.");

			var consumer = new AsyncEventingBasicConsumer(channel);
			consumer.ReceivedAsync += (model, ea) =>
			{
				byte[] body = ea.Body.ToArray();
				var message = Encoding.UTF8.GetString(body);
				Console.WriteLine($"Получено сообщение: {message}");
				return Task.CompletedTask;
			};

			await channel.BasicConsumeAsync(queueName, autoAck: true, consumer: consumer);

			Console.WriteLine(" Press [enter] to exit.");
			Console.ReadLine();
		}
	}
}
