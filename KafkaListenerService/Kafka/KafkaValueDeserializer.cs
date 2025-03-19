using Confluent.Kafka;
using System.Text.Json;

namespace KafkaListenerService.Kafka
{
    /// <summary>
    /// Класс для десериализации получаемых сообщений из Kafka.
    /// </summary>
    /// <typeparam name="TMessage">Объект принимаемого сообщения.</typeparam>
    public class KafkaValueDeserializer<TMessage> : IDeserializer<TMessage>
    {
		/// <summary>
		/// Метод для десериализации получаемых сообщений из Kafka для реализации IDeserializer<TMessage>.
		/// </summary>
		/// <param name="data">Данные, которые десериализуем.</param>
		/// <param name="isNull">Может ли значение быть null.</param>
		/// <param name="context">Контект операции десериализации.</param>
		/// <returns>Объект результата десериализации принимаемого сообщения.</returns>
		public TMessage Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            return JsonSerializer.Deserialize<TMessage>(data)!;
        }
    }
}
