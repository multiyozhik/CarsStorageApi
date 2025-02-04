using System.Security.Claims;

namespace CarsStorage.BLL.Abstractions.Interfaces
{
	public interface ITokensService
	{
		public string GetAccessToken(IEnumerable<Claim> claims, out DateTime accessTokenExpires);
		public string GetRefreshToken();
		public ClaimsPrincipal GetClaimsPrincipalFromExperedTokenWithValidation(string experedToken);
	}
}
