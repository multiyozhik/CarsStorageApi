namespace CarsStorage.DAL.Utils
{
	public interface IPasswordHasher
	{
		public (string hash, string salt) HashPassword(string password);
		public bool VerifyPassword(string password, string storedHash, string storedSalt);
	}
}
