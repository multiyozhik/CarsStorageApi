using CarsStorage.Abstractions.ModelsDTO.Token;

namespace CarsStorage.Abstractions.DAL.Repositories
{
	public interface ITokensRepository
	{
		public Task<JWTTokenDTO> GetTokenByUserId(int userId);
		public Task<JWTTokenDTO> UpdateToken(int userId, JWTTokenDTO jwtTokenDTO);
		public Task<int> ClearToken(string accessToken);
	}
}
