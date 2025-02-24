using System.Security.Cryptography;

namespace CarsStorage.BLL.Services.Utils
{
	/// <summary>
	/// Класс для хеширования пароля. 
	/// </summary>
	public class PasswordHasher : IPasswordHasher
	{
		/// <summary>
		/// Метод хеширования пароля с использованием Rfc2898DeriveBytes и рандомной соли с помощью RandomNumberGenerator.
		/// </summary>
		/// <returns>Объект пароля.</returns>
		public Password HashPassword(string password)
		{
			var salt = GenerateSalt();
			var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, 10000, HashAlgorithmName.SHA256, 32);
			return new Password(Convert.ToBase64String(hash), Convert.ToBase64String(salt));
		}


		/// <summary>
		/// Метод для проверки входящего пароля на соответствие хранимому паролю в БД. 
		/// </summary>
		/// <param name="password">Строка проверяемого пароля.</param>
		/// <param name="storedHash">Строка хеша пароля, хранимого в БД.</param>
		/// <param name="storedSalt">Строка соли для пароля, хранимого в  БД.</param>
		/// <returns>Булево значение соответствия проверяемого пароля хранимому паролю в БД.</returns>
		public bool VerifyPassword(string password, string storedHash, string storedSalt)
		{
			var salt = Convert.FromBase64String(storedSalt);
			var hash = Convert.FromBase64String(storedHash);
			var newHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, 10000, HashAlgorithmName.SHA256, 32);
			return hash.SequenceEqual(newHash);
		}


		/// <summary>
		/// Метод генерации соли с помощью генератора рандомных чисел (побайтно).
		/// </summary>
		/// <param name="size">Размер массива байтов.</param>
		/// <returns>Соль в виде массива байтов.</returns>
		private static byte[] GenerateSalt(int size = 16)
		{
			byte[] salt = new byte[size];
			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(salt);
			}
			return salt;
		}
	}
}
