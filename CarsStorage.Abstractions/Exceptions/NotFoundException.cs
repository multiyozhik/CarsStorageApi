namespace CarsStorage.Abstractions.Exceptions
{
	/// <summary>
	/// Класс исключения при ненайденном запрошенном ресурсе.
	/// </summary>
	/// <param name="message">Сообщение исключения.</param>
	public class NotFoundException(string message) : Exception(message)
	{
	}
}
