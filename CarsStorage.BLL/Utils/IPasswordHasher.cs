namespace CarsStorage.BLL.Services.Utils
{
	/// <summary>
	/// Интерфейс для хеширования пароля.
	/// </summary>
	public interface IPasswordHasher
	{
		/// <summary>
		/// Метод для получения объекта пароля по строке пароля.
		/// </summary>
		/// <param name="password">Строка пароля.</param>
		/// <returns>Объект пароля.</returns>
		public Password HashPassword(string password);


		/// <summary>
		/// Метод для проверки входящего пароля на соответствие хранимому паролю в БД. 
		/// </summary>
		/// <param name="password">Строка проверяемого пароля.</param>
		/// <param name="storedHash">Строка хеша пароля, хранимого в БД.</param>
		/// <param name="storedSalt">Строка соли для пароля, хранимого в  БД.</param>
		/// <returns>Булево значение соответствия проверяемого пароля хранимому паролю в БД.</returns>
		public bool VerifyPassword(string password, string storedHash, string storedSalt);
	}
}
