namespace CarsStorage.BLL.Services.Utils
{
	/// <summary>
	/// Класс пароля (свойства хеш и соль) используется в БД как свойство сущности пользователя.
	/// </summary>
	public class Password(string hash, string salt)
	{
		public string Hash { get; set; } = hash;
		public string Salt { get; set; } = salt;
	}
}
