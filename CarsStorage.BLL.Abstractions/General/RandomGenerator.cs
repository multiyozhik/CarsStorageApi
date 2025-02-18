using System.Security.Cryptography;

namespace CarsStorage.Abstractions.General
{
	public static class RandomGenerator
	{
		public static string GenerateState()
		{
			byte[] randomBytes = new byte[32];
			using var rng = RandomNumberGenerator.Create();
			rng.GetBytes(randomBytes);
			return Convert.ToBase64String(randomBytes);
		}
	}
}
