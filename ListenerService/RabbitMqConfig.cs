namespace ListenerService
{
	/// <summary>
	/// Класс для конфигураций RabbitMq.
	/// </summary>
	public class RabbitMqConfig
	{
		/// <summary>
		/// Строка имени хоста.
		/// </summary>
		public string HostName { get; set; } = string.Empty;

		/// <summary>
		/// Порт хоста.
		/// </summary>
		public int Port { get; set; }

		/// <summary>
		/// Логин пользователя RabbitMq.
		/// </summary>
		public string UserName { get; set; } = string.Empty;

		/// <summary>
		/// Пароль пользователя RabbitMq.
		/// </summary>
		public string Password { get; set; } = string.Empty;

		/// <summary>
		/// Наименование обменника.
		/// </summary>
		public string ExchangeName { get; set; } = string.Empty;
	}
}
