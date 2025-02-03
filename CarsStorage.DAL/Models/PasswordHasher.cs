using System.Security.Cryptography;

namespace CarsStorage.DAL.Models
{
	/// <summary>
	/// Класс для  хеширования пароля с использованием Rfc2898DeriveBytes и рандомной соли с помощью RandomNumberGenerator. 
	/// </summary>
	public class PasswordHasher
	{
		/// <summary>
		/// Метод хеширования пароля с использованием Rfc2898DeriveBytes и рандомной соли с помощью RandomNumberGenerator.
		/// </summary>
		/// <param name="password"></param>
		/// <returns>Возвращает хеш и соль пароля.</returns>
		public (string hash, string salt) HashPassword(string password)
		{
			var salt = GenerateSalt();
			var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, 10000, HashAlgorithmName.SHA256, 32);
			return (Convert.ToBase64String(hash), Convert.ToBase64String(salt));
		}


		/// <summary>
		/// Метод проверки соответствия проверяемого пароля хранимому паролю (поэлементное сравнение массива байтов проверяемого пароля, захешированного хранимой солью, с захешированным паролем в БД).
		/// </summary>
		/// <param name="password">Проверяемый пароль.</param>
		/// <param name="storedHash">Захешированный пароль, сохраненный в БД, с которым будет сравниваться хеш проверяемого пароля.</param>
		/// <param name="storedSalt">Соль, хранимая вместе с захешированным паролем в БД.</param>
		/// <returns>Возвращает булево значение соответствия проверяемого пароля сохраненному паролю в БД.</returns>
		public bool VerifyPassword(string password, string storedHash, string storedSalt)
		{
			var salt = Convert.FromBase64String(storedSalt);
			var hash = Convert.FromBase64String(storedHash);
			var newHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, 10000, HashAlgorithmName.SHA256,32);
			return hash.SequenceEqual(newHash);
		}


		/// <summary>
		/// Метод генерации соли с помощью генератора рандомных чисел (побайтно).
		/// </summary>
		/// <param name="size"></param>
		/// <returns>Возвращает соль в виде массива байтов.</returns>
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
