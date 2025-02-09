using CarsStorage.BLL.Abstractions.General;
using System.Security.Claims;

namespace CarsStorage.BLL.Abstractions.Services
{
	/// <summary>
	/// Интерфейс для сервиса токенов.
	/// </summary>
	public interface ITokensService
	{
		public Task<ServiceResult<string>> GetAccessToken(IEnumerable<Claim> claims);
		public Task<ServiceResult<string>> GetRefreshToken();
		public Task<ServiceResult<ClaimsPrincipal>> GetClaimsPrincipalFromExperedToken(string experedToken);
	}
}
