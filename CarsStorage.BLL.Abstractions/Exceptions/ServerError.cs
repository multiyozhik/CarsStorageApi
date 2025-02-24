namespace CarsStorage.Abstractions.Exceptions
{
	/// <summary>
	/// Класс исключения при ошибке на сервере.
	/// </summary>
	/// <param name="message">Сообщение исключения.</param>
	public class ServerException(string message) : Exception(message)
	{
	}
}
