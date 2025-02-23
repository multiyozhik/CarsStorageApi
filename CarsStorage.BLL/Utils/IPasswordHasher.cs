namespace CarsStorage.BLL.Services.Utils
{
	/// <summary>
	/// Интерфейс для  хеширования пароля - методы получения строки хеша и соли, проверки входящего пароля на соответствие хранимому паролю в БД. 
	/// </summary>
	public interface IPasswordHasher
	{
		public Password HashPassword(string password);
		public bool VerifyPassword(string password, string storedHash, string storedSalt);
	}
}
