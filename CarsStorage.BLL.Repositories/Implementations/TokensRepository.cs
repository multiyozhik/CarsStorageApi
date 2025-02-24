using CarsStorage.Abstractions.DAL.Repositories;
using CarsStorage.DAL.DbContexts;
using CarsStorage.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CarsStorage.DAL.Repositories.Implementations
{
	public class TokensRepository(AppDbContext dbContext) : ITokensRepository
	{
		/// <summary>
		/// Метод возвращает токен по id пользователя.
		/// </summary>
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
		/// Метод обновляет токен для пользователя с id.
		/// </summary>
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
		/// Метод очищает токен в БД при выходе пользователя из системы.
		/// </summary>
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
