using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CarsStorage.BLL.Abstractions.Interfaces
{
    public interface ITokenService
    {
        public string GetAccessToken(IEnumerable<Claim> claims, out DateTime expires);
        public string GetRefreshToken();
        public ClaimsPrincipal GetClaimsPrincipalFromExperedToken(string experedToken);
    }
}
