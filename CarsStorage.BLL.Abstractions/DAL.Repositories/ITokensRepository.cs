using CarsStorage.DAL.Models;

namespace CarsStorage.Abstractions.DAL.Repositories
{
	public interface ITokensRepository
	{
		public Task<JWTToken> GetTokenByUserId(int userId);
		public Task<JWTToken> UpdateToken(int userId, JWTToken jwtToken);
		public Task<int> ClearToken(string accessToken);
	}
}
