using System.Net.WebSockets;

namespace CarsStorage.BLL.Services.Utils
{
	/// <summary>
	/// Интерфейс для соединения WebSocket.
	/// </summary>
	public interface IWebSocketHandler
	{
		/// <summary>
		/// Метод обработки при WebSocket соединении, в случае проведения технических работ рассылка уведомления с задержкой 5 мин.
		/// </summary>
		/// <param name="webSocket">Объект WebSocket класса.</param>
		public Task HandleWebSocketConnection(WebSocket webSocket);
	}
}
