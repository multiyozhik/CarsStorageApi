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
	public class AuthenticateService(IUsersRepository usersRepository, ITokensService tokenService) : IAuthenticateService
	{
		/// <summary>
		/// Метод сервиса для входа пользователя в приложении и возврата токена клиенту.
		/// </summary>
		public async Task<ServiceResult<JWTTokenDTO>> LogIn(UserLoginDTO userLoginDTO) 
		{
			try
			{
				await usersRepository.IsUserValid(userLoginDTO);
				var userDTO = await usersRepository.GetUserWithRoles(userLoginDTO);			
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

				jwtTokenDTO = await usersRepository.UpdateToken(userDTO.Id, jwtTokenDTO);
				return new ServiceResult<JWTTokenDTO>(jwtTokenDTO);				
			}
			catch (Exception exception)
			{
				return new ServiceResult<JWTTokenDTO>(new UnauthorizedAccessException(exception.Message));
			}
		}



		/// <summary>
		/// Метод сервиса для доступа аутентифицированного пользователя в приложение.
		/// </summary>
		public async Task<ServiceResult<JWTTokenDTO>> LogInAuthUser(GitHubUserDTO gitHubUser, List<string> initialRoleNamesList)
		{
			try
			{
				var userDTO = await usersRepository.GetUserWithRoles(gitHubUser);




				if (isExist)
					usersRepository.Create(mapper.Map<userCreaterDTO>(gitHubUser));
				var userCreaterDTO = new UserCreaterDTO() { UserName }
				LogIn(mapper.Map<UserDTO>(userCreaterDTO));
				return new ServiceResult<UserDTO>(userDTO);
			}
			catch (Exception exception)
			{
				return new ServiceResult<UserDTO>(new NotFoundException(exception.Message));
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
				jwtTokenDTO = await usersRepository.GetTokenByUserId(userDTO.Id);
				var principal = tokenService.GetClaimsPrincipalFromExperedToken(jwtTokenDTO.AccessToken);

				var accessToken = tokenService.GetAccessToken(principal.Result.Claims.ToList());
				var refreshToken = tokenService.GetRefreshToken();

				var newRefreshingToken = new JWTTokenDTO()
				{
					AccessToken = accessToken.Result,
					RefreshToken = refreshToken.Result
				};
				await usersRepository.UpdateToken(userDTO.Id, newRefreshingToken);
				return new ServiceResult<JWTTokenDTO>(newRefreshingToken);
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
				var userId = await usersRepository.ClearToken(accessToken);
				return new ServiceResult<int>(userId);
			}
			catch (Exception exception)
			{
				return new ServiceResult<int>(new BadRequestException(exception.Message));
			}
		}
	}
}
