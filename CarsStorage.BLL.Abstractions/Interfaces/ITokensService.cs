using CarsStorage.BLL.Abstractions.Models;
using System.Security.Claims;

namespace CarsStorage.BLL.Abstractions.Interfaces
{
	/// <summary>
	/// Интерфейс для сервиса токенов.
	/// </summary>
	public interface ITokensService
	{
		public Task<ServiceResult<string>> GetAccessToken(IEnumerable<Claim> claims);
		public Task<ServiceResult<string>> GetRefreshToken();
		public Task<ServiceResult<ClaimsPrincipal>> GetClaimsPrincipalFromExperedTokenWithValidation(string experedToken);
	}
}
