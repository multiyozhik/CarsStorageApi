using CarsStorage.BLL.Abstractions.Config;
using CarsStorage.BLL.Abstractions.Exceptions;
using CarsStorage.BLL.Abstractions.Interfaces;
using CarsStorage.BLL.Abstractions.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CarsStorage.BLL.Implementations.Services
{
	/// <summary>
	/// Сервис для методов токенов доступа.
	/// </summary>
	/// <param name="jwtConfig"></param>
	public class TokensService(IOptions<JWTConfig> jwtOptions) : ITokensService
	{
		/// <summary>
		/// Метод генерации токена доступа.
		/// </summary>
		/// <param name="claims">Коллекция клаймов, на основе которых генерируется токен.</param>
		/// <param name="expires">Возвращаемое значение даты и времени истечения жизни сгенерированного токена.</param>
		/// <returns></returns>
		public async Task<ServiceResult<string>> GetAccessToken(IEnumerable<Claim> claims)
		{
			try
			{
				var jwtConfig = jwtOptions.Value;
				var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key));
				var accessTokenExpires = DateTime.Now.AddMinutes(jwtConfig.ExpireMinutes);
				var accessToken = new JwtSecurityToken(
					issuer: jwtConfig.Issuer,
					audience: jwtConfig.Audience,
					claims: claims,
					expires: accessTokenExpires,
					signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
				var tokenHandler = new JwtSecurityTokenHandler();
				return new ServiceResult<string>(tokenHandler.WriteToken(accessToken), null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<string>(null, new BadRequestException(exception.Message));
			}

		}


		/// <summary>
		/// Метод обновления токена доступа, генерация генератором случайных чисел с преобразованием в строку.
		/// </summary>
		/// <returns></returns>
		public async Task<ServiceResult<string>> GetRefreshToken()
		{
			try
			{
				var randomNumber = new byte[32];
				using var randomNumberGenerator = RandomNumberGenerator.Create();
				randomNumberGenerator.GetBytes(randomNumber);
				return new ServiceResult<string>(Convert.ToHexString(randomNumber), null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<string>(null, new ServerException(exception.Message));
			}
		}


		/// <summary>
		/// Метод получения из проверяемого токена доступа с истекшим сроком жизни объекта ClaimsPrincipal для получения клаймов.
		/// </summary>
		/// <param name="experedToken">Токен доступа с истекшим сроком жизни</param>
		/// <returns></returns>
		/// <exception cref="SecurityTokenException">Исключение о невалидноcти проверяемого токена (когда время его жизни прошло).</exception>
		public async Task<ServiceResult<ClaimsPrincipal>> GetClaimsPrincipalFromExperedTokenWithValidation(string experedToken)
		{
			try
			{
				var jwtConfig = jwtOptions.Value;
				var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key));
				var tokenHandler = new JwtSecurityTokenHandler();
				var validationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = jwtConfig.ValidateIssuer,
					ValidateAudience = jwtConfig.ValidateAudience,
					ValidateIssuerSigningKey = jwtConfig.ValidateIssuerSigningKey,
					ValidIssuer = jwtConfig.Issuer,
					ValidAudience = jwtConfig.Audience,
					IssuerSigningKey = key
				};
				var claimsPrincipal = tokenHandler.ValidateToken(experedToken, validationParameters, out SecurityToken securityToken);

				var jwtSecurityToken = securityToken as JwtSecurityToken;
				if (jwtSecurityToken is null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
					throw new SecurityTokenException("Неверный токен");
				return new ServiceResult<ClaimsPrincipal>(claimsPrincipal, null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<ClaimsPrincipal>(null, new BadRequestException(exception.Message));
			}
		}
	}
}
