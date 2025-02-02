using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CarsStorage.BLL.Abstractions.Interfaces
{
    public interface ITokensService
    {
        public string GetAccessToken(IEnumerable<Claim> claims, out DateTime accessTokenExpires);
        public string GetRefreshToken();
        public ClaimsPrincipal GetClaimsPrincipalFromExperedTokenWithValidation(string experedToken);
	}
}
