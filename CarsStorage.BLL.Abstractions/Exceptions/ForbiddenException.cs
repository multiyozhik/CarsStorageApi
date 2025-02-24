namespace CarsStorage.Abstractions.Exceptions
{
	/// <summary>
	/// Класс исключения при запрете доступа к запрошенному ресурсу.
	/// </summary>
	/// <param name="message">Сообщение исключения.</param>
	public class ForbiddenException(string message) : Exception(message)
	{
	}
}
