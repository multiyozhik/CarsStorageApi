using CarsStorage.Abstractions.General;
using CarsStorage.Abstractions.ModelsDTO.Token;
using CarsStorage.Abstractions.ModelsDTO.User;

namespace CarsStorage.Abstractions.BLL.Services
{
	/// <summary>
	/// Интерфейс для аутентификации пользователей.
	/// </summary>
	public interface IAuthenticateService
	{
		public Task<ServiceResult<JWTTokenDTO>> LogIn(UserLoginDTO userLoginDTO);
		public Task<ServiceResult<JWTTokenDTO>> RefreshToken(JWTTokenDTO jwtTokenDTO);
		public Task<ServiceResult<int>> LogOut(string accessToken);
	}
}
