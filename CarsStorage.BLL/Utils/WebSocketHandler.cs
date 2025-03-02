using CarsStorage.Abstractions.BLL.Services;
using System.Net.WebSockets;

namespace CarsStorage.BLL.Services.Utils
{
	/// <summary>
	/// Класс для соединения WebSocket.
	/// </summary>
	/// <param name="technicalWorksService">Сервис по проведению технических работ.</param>
	public class WebSocketHandler(ITechnicalWorksService technicalWorksService): IWebSocketHandler
	{
		/// <summary>
		/// Метод обработки при WebSocket соединении, в случае проведения технических работ рассылка уведомления с задержкой 5 мин.
		/// </summary>
		/// <param name="webSocket">Объект WebSocket класса.</param>
		public async Task HandleWebSocketConnection(WebSocket webSocket)
		{
			while (webSocket.State == WebSocketState.Open)
			{
				if (await technicalWorksService.HasTechnicalWorks())
				{
					var message = "Ведутся технические работы.";
					var bytes = System.Text.Encoding.UTF8.GetBytes(message);
					var buffer = new ArraySegment<byte>(bytes);
					await webSocket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
				}
				await Task.Delay(TimeSpan.FromMinutes(5));
			}
		}
	}
}
