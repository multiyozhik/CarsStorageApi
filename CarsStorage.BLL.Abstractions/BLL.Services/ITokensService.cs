using CarsStorage.Abstractions.General;
using CarsStorage.Abstractions.ModelsDTO.Token;
using System.Security.Claims;

namespace CarsStorage.Abstractions.BLL.Services
{
	/// <summary>
	/// Интерфейс для сервиса токенов.
	/// </summary>
	public interface ITokensService
	{
		public ServiceResult<string> GetAccessToken(IEnumerable<Claim> claims);
		public ServiceResult<string> GetRefreshToken();
		public ServiceResult<ClaimsPrincipal> GetClaimsPrincipalFromExperedToken(string experedToken);
		public Task<ServiceResult<JWTTokenDTO>> GetTokenByUserId(int userId);
		public Task<ServiceResult<JWTTokenDTO>> UpdateToken(int userId, JWTTokenDTO jwtTokenDTO);
		public Task<ServiceResult<int>> ClearToken(string accessToken);
	}
}
