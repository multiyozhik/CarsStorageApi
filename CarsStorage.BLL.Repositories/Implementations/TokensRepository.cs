using CarsStorage.Abstractions.DAL.Repositories;
using CarsStorage.Abstractions.ModelsDTO.Token;
using CarsStorage.DAL.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace CarsStorage.DAL.Repositories.Implementations
{
	public class TokensRepository(AppDbContext dbContext) : ITokensRepository
	{
		/// <summary>
		/// Метод возвращает токен по id пользователя.
		/// </summary>
		public async Task<JWTTokenDTO> GetTokenByUserId(int userId)
		{
			var userEntity = await dbContext.Users.FirstOrDefaultAsync(u => u.UserEntityId == userId)
				?? throw new Exception("Пользователь с заданным id не найден");
			return new JWTTokenDTO
			{
				AccessToken = userEntity.AccessToken ?? throw new Exception("Токен доступа не определен."),
				RefreshToken = userEntity.RefreshToken ?? throw new Exception("Токен обновления не определен.")
			};
		}


		/// <summary>
		/// Метод обновляет токен для пользователя с id.
		/// </summary>
		public async Task<JWTTokenDTO> UpdateToken(int userId, JWTTokenDTO jwtTokenDTO)
		{
			var userEntity = await dbContext.Users.FirstOrDefaultAsync(u => u.UserEntityId == userId)
				?? throw new Exception("Пользователь с заданным Id не найден");
			userEntity.AccessToken = jwtTokenDTO.AccessToken;
			userEntity.RefreshToken = jwtTokenDTO.RefreshToken;
			dbContext.Update(userEntity);
			await dbContext.SaveChangesAsync();
			return new JWTTokenDTO { AccessToken = jwtTokenDTO.AccessToken, RefreshToken = jwtTokenDTO.RefreshToken };
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
