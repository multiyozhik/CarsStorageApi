using CarsStorage.BLL.Services.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarsStorage.BLL.Services.Kafka
{
	/// <summary>
	/// Класс для определения метода расширения для IServiceCollection для добавления сервиса producer сообщений в Kafka.
	/// </summary>
	public static class Extentions
	{
		/// <summary>
		/// Класс для определения метода расширения для IServiceCollection для добавления сервиса producer сообщений в Kafka.
		/// </summary>
		/// <typeparam name="TMessage">Универсальный параметр - объект получаемого сообщения.</typeparam>
		/// <param name="services">Коллекция сервисов типа IServiceCollection.</param>
		/// <param name="configurationSection">Объект конфигураций типа IConfigurationSection.</param>
		/// <returns>Коллекция сервисов типа IServiceCollection, в которую добавлен сервис producer.</returns>
		public static IServiceCollection AddProducer<TMessage>(this IServiceCollection services, IConfigurationSection configurationSection)
		{
			services.Configure<KafkaConfig>(configurationSection);
			services.AddSingleton<IKafkaProducer<TMessage>, KafkaProducer<TMessage>>();
			return services;
		}
	}
}
