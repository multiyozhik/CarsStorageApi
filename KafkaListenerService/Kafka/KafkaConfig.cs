namespace KafkaListenerService.Kafka
{
    /// <summary>
    /// Класс конфигураций Kafka.
    /// </summary>
    public class KafkaConfig
    {
        /// <summary>
        /// url-адрес подключения к Kafka.
        /// </summary>
        public string BootstrapService { get; set; } = string.Empty;

        /// <summary>
        /// Топик сообщений.
        /// </summary>
        public string Topic { get; set; } = string.Empty;

        /// <summary>
        /// Id для группы сообщений.
        /// </summary>
        public string GroupId { get; set; } = string.Empty;
    }
}
