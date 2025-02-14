using CarsStorage.Abstractions.General;
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
	}
}
