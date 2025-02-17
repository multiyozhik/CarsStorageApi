using CarsStorage.Abstractions.BLL.Services;
using CarsStorage.Abstractions.DAL.Repositories;
using CarsStorage.Abstractions.Exceptions;
using CarsStorage.Abstractions.General;
using CarsStorage.Abstractions.ModelsDTO.Token;
using CarsStorage.BLL.Services.Config;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace CarsStorage.BLL.Services.Services
{
	/// <summary>
	/// Сервис для методов токенов доступа.
	/// </summary>
	/// <param name="jwtConfig"></param>
	public class TokensService(ITokensRepository tokensRepository, IOptions<JWTConfig> jwtOptions) : ITokensService
	{
		/// <summary>
		/// Метод генерации токена доступа.
		/// </summary>
		/// <param name="claims">Коллекция клаймов, на основе которых генерируется токен.</param>
		/// <param name="expires">Возвращаемое значение даты и времени истечения жизни сгенерированного токена.</param>
		/// <returns></returns>
		public ServiceResult<string> GetAccessToken(IEnumerable<Claim> claims)
		{
			try
			{
				var jwtConfig = jwtOptions.Value;
				var key = new SymmetricSecurityKey(Convert.FromBase64String(jwtConfig.Key));
				var accessTokenExpires = DateTime.Now.AddMinutes(jwtConfig.ExpireMinutes);
				var accessToken = new JwtSecurityToken(
					issuer: jwtConfig.Issuer,
					audience: jwtConfig.Audience,
					claims: claims,
					expires: accessTokenExpires,
					signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
				var tokenHandler = new JwtSecurityTokenHandler();
				return new ServiceResult<string>(tokenHandler.WriteToken(accessToken));
			}
			catch (Exception exception)
			{
				return new ServiceResult<string>(new BadRequestException(exception.Message));
			}

		}


		/// <summary>
		/// Метод обновления токена доступа, генерация генератором случайных чисел с преобразованием в строку.
		/// </summary>
		/// <returns></returns>
		public ServiceResult<string> GetRefreshToken()
		{
			try
			{
				var randomNumber = new byte[32];
				using var randomNumberGenerator = RandomNumberGenerator.Create();
				randomNumberGenerator.GetBytes(randomNumber);
				return new ServiceResult<string>(Convert.ToHexString(randomNumber));
			}
			catch (Exception exception)
			{
				return new ServiceResult<string>(new ServerException(exception.Message));
			}
		}


		/// <summary>
		/// Метод получения из токена доступа с истекшим сроком жизни объекта ClaimsPrincipal для получения клаймов.
		/// </summary>
		public ServiceResult<ClaimsPrincipal> GetClaimsPrincipalFromExperedToken(string experedToken)
		{
			try
			{
				var jwtConfig = jwtOptions.Value;
				var key = new SymmetricSecurityKey(Convert.FromBase64String(jwtConfig.Key));
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

				if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
					throw new SecurityTokenException("Неверный токен");
				return new ServiceResult<ClaimsPrincipal>(claimsPrincipal);
			}
			catch (Exception exception)
			{
				return new ServiceResult<ClaimsPrincipal>(new BadRequestException(exception.Message));
			}
		}

		/// <summary>
		/// Метод возвращает токен по id пользователя.
		/// </summary>
		public async Task<ServiceResult<JWTTokenDTO>> GetTokenByUserId(int userId)
		{
			try 
			{
				var jwtToken = await tokensRepository.GetTokenByUserId(userId);
				return new ServiceResult<JWTTokenDTO>(jwtToken);
			}
			catch (Exception exception)
			{
				return new ServiceResult<JWTTokenDTO>(new NotFoundException(exception.Message));
			}
		}


		/// <summary>
		/// Метод обновляет токен для пользователя с id.
		/// </summary>
		public async Task<ServiceResult<JWTTokenDTO>> UpdateToken(int userId, JWTTokenDTO jwtTokenDTO)
		{
			try
			{
				var jwtToken = await tokensRepository.UpdateToken(userId, jwtTokenDTO);
				return new ServiceResult<JWTTokenDTO>(jwtToken);
			}
			catch (Exception exception)
			{
				return new ServiceResult<JWTTokenDTO>(new BadRequestException(exception.Message));
			}
		}


		/// <summary>
		/// Метод очищает токен в БД при выходе пользователя из системы.
		/// </summary>
		public async Task<ServiceResult<int>> ClearToken(string accessToken)
		{
			try
			{
				var userId = await tokensRepository.ClearToken(accessToken);
				return new ServiceResult<int>(userId);
			}
			catch (Exception exception)
			{
				return new ServiceResult<int>(new BadRequestException(exception.Message));
			}
		}
	}
}
