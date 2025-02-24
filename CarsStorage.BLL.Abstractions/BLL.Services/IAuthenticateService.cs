using CarsStorage.Abstractions.General;
using CarsStorage.Abstractions.ModelsDTO.Token;
using CarsStorage.Abstractions.ModelsDTO.User;

namespace CarsStorage.Abstractions.BLL.Services
{
	/// <summary>
	/// Интерфейс для сервиса аутентификации пользователей.
	/// </summary>
	public interface IAuthenticateService
	{
		/// <summary>
		/// Метод для логина пользователей.
		/// </summary>
		/// <param name="userLoginDTO">Объект данных пользователя для аутентификации.</param>
		/// <returns>Объект токена доступа.</returns>
		public Task<ServiceResult<JWTTokenDTO>> LogIn(UserLoginDTO userLoginDTO);


		/// <summary>
		/// Метод для логина пользователей, аутентифицированных на стороннем провайдере аутентификации.
		/// </summary>
		/// <param name="authUserDataDTO">Объект с данными пользователя, полученных от стороннего провайдера аутентификации.</param>
		/// <returns>Объект токена доступа.</returns>
		public Task<ServiceResult<JWTTokenDTO>> LogInAuthUser(AuthUserDataDTO authUserDataDTO);


		/// <summary>
		/// Метод для обновления токена доступа.
		/// </summary>
		/// <param name="jwtTokenDTO">Объект токена доступа, который будет обновляться.</param>
		/// <returns>Объект токена доступа.</returns>
		public Task<ServiceResult<JWTTokenDTO>> RefreshToken(JWTTokenDTO jwtTokenDTO);


		/// <summary>
		/// Метод для разлогинивания пользователя.
		/// </summary>
		/// <param name="accessToken">Объект токена доступа пользователя, который выходит из приложения.</param>
		/// <returns>Индентификатор объекта пользователя, который выходит из приложения.</returns>
		public Task<ServiceResult<int>> LogOut(string accessToken);
	}
}
