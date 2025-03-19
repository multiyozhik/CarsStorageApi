using Confluent.Kafka;
using System.Text.Json;

namespace CarsStorage.BLL.Services.Kafka
{
	/// <summary>
	/// Класс для сериализации данных сообщения универсального типа TMessage в массив байтов.
	/// </summary>
	/// <typeparam name="TMessage">Данные собщения.</typeparam>
	public class KafkaJsonSerializer<TMessage> : ISerializer<TMessage>
	{
		/// <summary>
		/// Метод сериализации данных сообщения универсального типа TMessage в массив байтов.
		/// </summary>
		/// <param name="data">Данные для сериализации в массив байтов.</param>
		/// <param name="context">Объект контекста операции сериализации.</param>
		/// <returns>Массив байтов.</returns>
		public byte[] Serialize(TMessage data, SerializationContext context)
		{
			return JsonSerializer.SerializeToUtf8Bytes(data);
		}
	}
}
