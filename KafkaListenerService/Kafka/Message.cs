namespace KafkaListenerService.Kafka
{
    /// <summary>
    /// Класс принимаемого сообщения от Kafka.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Время получения сообщения.
        /// </summary>
        public string TimeStamp { get; set; } = DateTime.Now.ToShortTimeString();

        /// <summary>
        /// Текст принимаемого сообщения.
        /// </summary>
        public string Text { get; set; } = string.Empty;
    }
}
