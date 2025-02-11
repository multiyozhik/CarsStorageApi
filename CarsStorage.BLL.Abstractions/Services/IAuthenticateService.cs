using CarsStorage.BLL.Abstractions.General;
using CarsStorage.BLL.Abstractions.ModelsDTO.Token;
using CarsStorage.BLL.Abstractions.ModelsDTO.User;

namespace CarsStorage.BLL.Abstractions.Services
{
	/// <summary>
	/// Интерфейс для аутентификации пользователей.
	/// </summary>
	public interface IAuthenticateService
	{
		public Task<ServiceResult<UserDTO>> Register(UserRegisterDTO userRegisterDTO);
		public Task<ServiceResult<JWTTokenDTO>> LogIn(UserLoginDTO userLoginDTO);
		public Task<ServiceResult<JWTTokenDTO>> RefreshToken(JWTTokenDTO jwtTokenDTO);
		public Task<ServiceResult<int>> LogOut(string accessToken);
	}
}
