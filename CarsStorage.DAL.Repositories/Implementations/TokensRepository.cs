using CarsStorage.Abstractions.DAL.Repositories;
using CarsStorage.DAL.DbContexts;
using CarsStorage.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CarsStorage.DAL.Repositories.Implementations
{
	/// <summary>
	/// Класс репозитория токена.
	/// </summary>
	/// <param name="dbContext">Объект контекста данных.</param>
	public class TokensRepository(AppDbContext dbContext) : ITokensRepository
	{
		/// <summary>
		/// Метод для получения объекта токена доступа по идентификатору пользователя.
		/// </summary>
		/// <param name="userId">Идентификатор пользователя.</param>
		/// <returns>Объект токена доступа.</returns>
		public async Task<JWTToken> GetTokenByUserId(int userId)
		{
			var userEntity = await dbContext.Users.FirstOrDefaultAsync(u => u.UserEntityId == userId)
				?? throw new Exception("Пользователь с заданным id не найден");
			return new JWTToken
			{
				AccessToken = userEntity.AccessToken ?? throw new Exception("Токен доступа не определен."),
				RefreshToken = userEntity.RefreshToken ?? throw new Exception("Токен обновления не определен.")
			};
		}


		/// <summary>
		/// Метод для обновления объекта токена доступа.
		/// </summary>
		/// <param name="userId">Идентификатор пользователя.</param>
		/// <param name="jwtToken">Объект токена доступа для обновления значений.</param>
		/// <returns>Обновленный объект токена доступа.</returns>
		public async Task<JWTToken> UpdateToken(int userId, JWTToken jwtToken)
		{
			var userEntity = await dbContext.Users.FirstOrDefaultAsync(u => u.UserEntityId == userId)
				?? throw new Exception("Пользователь с заданным Id не найден");
			userEntity.AccessToken = jwtToken.AccessToken;
			userEntity.RefreshToken = jwtToken.RefreshToken;
			dbContext.Update(userEntity);
			await dbContext.SaveChangesAsync();
			return new JWTToken { AccessToken = jwtToken.AccessToken, RefreshToken = jwtToken.RefreshToken };
		}


		/// <summary>
		/// Метод для очистки объекта токена доступа (устанавливается null) по строке токена доступа.
		/// </summary>
		/// <param name="accessToken">Строка токена доступа.</param>
		/// <returns>Идентификатор пользователя, у которого очищены токены.</returns>
		public async Task<int> ClearToken(string accessToken)
		{
			var userEntity = await dbContext.Users.FirstOrDefaultAsync(u => u.AccessToken == accessToken)
				?? throw new Exception("Неверный токен");
			userEntity.RefreshToken = null;
			userEntity.AccessToken = null;
			dbContext.Update(userEntity);
			await dbContext.SaveChangesAsync();
			return userEntity.UserEntityId;
		}
	}
}
