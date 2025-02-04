using System.Security.Claims;

namespace CarsStorage.BLL.Abstractions.Interfaces
{
	/// <summary>
	/// Интерфейс для сервиса токенов.
	/// </summary>
	public interface ITokensService
	{
		public string GetAccessToken(IEnumerable<Claim> claims, out DateTime accessTokenExpires);
		public string GetRefreshToken();
		public ClaimsPrincipal GetClaimsPrincipalFromExperedTokenWithValidation(string experedToken);
	}
}
