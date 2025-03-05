using CarsStorage.DAL.Models;

namespace CarsStorage.Abstractions.DAL.Repositories
{
	/// <summary>
	/// Интерфейс для репозитория токенов.
	/// </summary>
	public interface ITokensRepository
	{
		/// <summary>
		/// Метод для получения объекта токена доступа по идентификатору пользователя.
		/// </summary>
		/// <param name="userId">Идентификатор пользователя.</param>
		/// <returns>Объект токена доступа.</returns>
		public Task<JWTToken> GetTokenByUserId(int userId);


		/// <summary>
		/// Метод для обновления объекта токена доступа.
		/// </summary>
		/// <param name="userId">Идентификатор пользователя.</param>
		/// <param name="jwtToken">Объект токена доступа для обновления значений.</param>
		/// <returns>Обновленный объект токена доступа.</returns>
		public Task<JWTToken> UpdateToken(int userId, JWTToken jwtToken);


		/// <summary>
		/// Метод для очистки объекта токена доступа (устанавливается null) по строке токена доступа.
		/// </summary>
		/// <param name="accessToken">Строка токена доступа.</param>
		/// <returns>Идентификатор пользователя, у которого очищены токены.</returns>
		public Task<int> ClearToken(string accessToken);
	}
}
