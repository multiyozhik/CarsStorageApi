namespace KafkaListenerService.Kafka
{
	/// <summary>
	/// Класс для определения метода расширения для IServiceCollection для добавления сервиса consumer сообщений от Kafka.
	/// </summary>
	public static class Extentions
    {
        /// <summary>
        /// Метод расширения для IServiceCollection для добавления сервиса consumer сообщений от Kafka.
        /// </summary>
        /// <typeparam name="TMessage">Универсальный параметр - объект получаемого сообщения.</typeparam>
        /// <typeparam name="THandler">Универсальный параметр - объект обработчика при получении сообщения.</typeparam>
        /// <param name="services">Коллекция сервисов типа IServiceCollection.</param>
        /// <param name="configurationSection">Объект конфигураций типа IConfigurationSection.</param>
        /// <returns>Коллекция сервисов типа IServiceCollection, в которую добавлен сервис consumer.</returns>
        public static IServiceCollection AddConsumer<TMessage, THandler>(this IServiceCollection services, IConfigurationSection configurationSection)
            where THandler : class, IMessageHandler<TMessage>
        {
            services.Configure<KafkaConfig>(configurationSection);
            services.AddHostedService<KafkaListener<TMessage>>();
            services.AddSingleton<IMessageHandler<TMessage>, THandler>();
            return services;
        }
    }
}
