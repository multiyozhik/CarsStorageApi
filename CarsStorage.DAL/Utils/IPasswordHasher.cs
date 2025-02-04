namespace CarsStorage.DAL.Utils
{
	/// <summary>
	/// Интерфейс для  хеширования пароля. 
	/// </summary>
	public interface IPasswordHasher
	{
		public (string hash, string salt) HashPassword(string password);
		public bool VerifyPassword(string password, string storedHash, string storedSalt);
	}
}
