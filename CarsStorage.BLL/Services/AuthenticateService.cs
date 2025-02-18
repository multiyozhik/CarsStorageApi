using CarsStorage.Abstractions.BLL.Services;
using CarsStorage.Abstractions.DAL.Repositories;
using CarsStorage.Abstractions.Exceptions;
using CarsStorage.Abstractions.General;
using CarsStorage.Abstractions.ModelsDTO;
using CarsStorage.Abstractions.ModelsDTO.Token;
using CarsStorage.Abstractions.ModelsDTO.User;
using CarsStorage.BLL.Abstractions.ModelsDTO.User;
using System.Security.Claims;

namespace CarsStorage.BLL.Services.Services
{
	/// <summary>
	/// Сервис для аутентификации.
	/// </summary>
	public class AuthenticateService(IUsersRepository usersRepository,  ITokensService tokenService) : IAuthenticateService
	{
		/// <summary>
		/// Метод сервиса для входа пользователя в приложение и возврата токена клиенту.
		/// </summary>
		public async Task<ServiceResult<JWTTokenDTO>> LogIn(UserLoginDTO userLoginDTO) 
		{
			try
			{
				await usersRepository.IsUserValid(userLoginDTO);
				var userDTO = await usersRepository.GetUserWithRoles(userLoginDTO);
				return new ServiceResult<JWTTokenDTO>(await GetJWTTokenDTO(userDTO));
			}
			catch (Exception exception)
			{
				return new ServiceResult<JWTTokenDTO>(new UnauthorizedAccessException(exception.Message));
			}
		}


		/// <summary>
		/// Метод сервиса для входа аутентифицированного пользователя в приложение.
		/// </summary>
		public async Task<ServiceResult<JWTTokenDTO>> LogInAuthUser(AuthUserData authUserData)
		{
			try
			{
				var userDTO = await usersRepository.GetUserByAuthUserData(authUserData);
				return new ServiceResult<JWTTokenDTO>(await GetJWTTokenDTO(userDTO));
			}
			catch (Exception exception)
			{
				return new ServiceResult<JWTTokenDTO>(new NotFoundException(exception.Message));
			}
		}


		/// <summary>
		/// Метод сервиса возвращает как результат объект токена с обновленным refresh токеном.
		/// </summary>
		public async Task<ServiceResult<JWTTokenDTO>> RefreshToken(JWTTokenDTO jwtTokenDTO)
		{
			//по refresh токену нашли userDTO, по userId возвращаем истекший access токен,
			//из кот. получаем список клаймов из ClaimsPrincipal для генерации нового access токена,
			//refreshToken - просто обновляется рандомная строка (без полезной нагрузки)
			try
			{
				var userDTO = await usersRepository.GetUserByRefreshToken(jwtTokenDTO.RefreshToken);
				var jwtTokenResult = await tokenService.GetTokenByUserId(userDTO.Id);

				if (!jwtTokenResult.IsSuccess)
					throw jwtTokenResult.ServiceError;

				var principal = tokenService.GetClaimsPrincipalFromExperedToken(jwtTokenResult.Result.AccessToken);

				var accessToken = tokenService.GetAccessToken(principal.Result.Claims.ToList());
				var refreshToken = tokenService.GetRefreshToken();

				var newRefreshingToken = new JWTTokenDTO()
				{
					AccessToken = accessToken.Result,
					RefreshToken = refreshToken.Result
				};
				var refreshTokenResult = await tokenService.UpdateToken(userDTO.Id, newRefreshingToken);
				if (!refreshTokenResult.IsSuccess)
					throw refreshTokenResult.ServiceError;
				return new ServiceResult<JWTTokenDTO>(refreshTokenResult.Result);
			}
			catch (Exception exception)
			{
				return new ServiceResult<JWTTokenDTO>(new UnauthorizedAccessException(exception.Message));
			}
		}


		/// <summary>
		/// Метод сервиса для выхода пользователя из приложения, возвращает как результат id вышедшего пользователя.
		/// </summary>
		public async Task<ServiceResult<int>> LogOut(string accessToken) 
		{
			try
			{
				var userIdResult = await tokenService.ClearToken(accessToken);
				if (!userIdResult.IsSuccess)
					throw userIdResult.ServiceError;
				return new ServiceResult<int>(userIdResult.Result);
			}
			catch (Exception exception)
			{
				return new ServiceResult<int>(new BadRequestException(exception.Message));
			}
		}


		/// <summary>
		/// Закрытый метод сервиса возвращает токен для входа пользователя в приложение.
		/// </summary>
		private async Task<JWTTokenDTO> GetJWTTokenDTO(UserDTO userDTO)
		{
			var roleClaims = userDTO.RolesList.SelectMany(role => role.RoleClaims).Distinct().ToList();
			var userClaims = new List<Claim> { new(ClaimTypes.Name, userDTO.UserName) };

			roleClaims.ForEach(roleClaim => userClaims.Add(new Claim(typeof(RoleClaimTypeBLL).ToString(), roleClaim.ToString())));

			var accessTokenFromService = tokenService.GetAccessToken(userClaims);
			var refreshTokenFromService = tokenService.GetRefreshToken();

			var jwtTokenDTO = new JWTTokenDTO()
			{
				AccessToken = accessTokenFromService.Result,
				RefreshToken = refreshTokenFromService.Result
			};
			var tokenServiceResult = await tokenService.UpdateToken(userDTO.Id, jwtTokenDTO);

			if (!tokenServiceResult.IsSuccess)
				throw tokenServiceResult.ServiceError;
			return tokenServiceResult.Result;
		}
	}
}
