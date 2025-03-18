namespace CarsStorage.BLL.Services.Config
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
		/// Порт хоста (по умолчанию 5672 для RabbitMq).
		/// </summary>
		public int Port { get; set; } = 5672;

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
