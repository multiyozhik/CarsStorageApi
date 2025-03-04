namespace CarsStorage.Abstractions.Exceptions
{
	/// <summary>
	/// Класс исключения при некорректном запросе серверу.
	/// </summary>
	/// <param name="message">Сообщение исключения.</param>
	public class BadRequestException(string message) : Exception(message)
	{
	}
}
