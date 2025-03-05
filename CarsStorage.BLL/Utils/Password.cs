namespace CarsStorage.BLL.Services.Utils
{
	/// <summary>
	/// Класс пароля.
	/// </summary>
	/// <param name="hash">Хеш пароля.</param>
	/// <param name="salt">Соль пароля.</param>
	public class Password(string hash, string salt)
	{
		/// <summary>
		/// Хеш пароля.
		/// </summary>
		public string Hash { get; set; } = hash;

		/// <summary>
		/// Соль пароля.
		/// </summary>
		public string Salt { get; set; } = salt;
	}
}
