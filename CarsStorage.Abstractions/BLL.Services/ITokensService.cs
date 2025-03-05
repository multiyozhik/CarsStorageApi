using CarsStorage.Abstractions.General;
using CarsStorage.Abstractions.ModelsDTO.Token;
using System.Security.Claims;

namespace CarsStorage.Abstractions.BLL.Services
{
	/// <summary>
	/// Интерфейс для сервиса токенов.
	/// </summary>
	public interface ITokensService
	{
		/// <summary>
		/// Метод для получения токена доступа.
		/// </summary>
		/// <param name="claims">Список утверждений для объекта пользователя.</param>
		/// <returns>Строка токена доступа.</returns>
		public ServiceResult<string> GetAccessToken(IEnumerable<Claim> claims);


		/// <summary>
		/// Метод для получения токена обновления.
		/// </summary>
		/// <returns>Строка токена обновления.</returns>
		public ServiceResult<string> GetRefreshToken();


		/// <summary>
		/// Метод для получения списка утверждений для пользователя из токена доступа с истекшим временем жизни.
		/// </summary>
		/// <param name="experedToken">Токен доступа с истекшим временем жизни.</param>
		/// <returns>Список утверждений для пользователя.</returns>
		public ServiceResult<ClaimsPrincipal> GetClaimsPrincipalFromExperedToken(string experedToken);


		/// <summary>
		/// Метод для получения объекта токена доступа по идентификатору пользователя.
		/// </summary>
		/// <param name="userId">Идентификатор объекта пользователя.</param>
		/// <returns>Объект токена доступа.</returns>
		public Task<ServiceResult<JWTTokenDTO>> GetTokenByUserId(int userId);


		/// <summary>
		/// Метод для обновления объекта токена доступа по идентификатору пользователя.
		/// </summary>
		/// <param name="userId">Идентификатор объекта пользователя.</param>
		/// <param name="jwtTokenDTO">Объект токена доступа.</param>
		/// <returns>обновленный объект токена доступа.</returns>
		public Task<ServiceResult<JWTTokenDTO>> UpdateToken(int userId, JWTTokenDTO jwtTokenDTO);


		/// <summary>
		/// Метод для очистки объекта токена доступа (устанавливается null) по строке токена доступа.
		/// </summary>
		/// <param name="accessToken">Строка токена доступа.</param>
		/// <returns>Идентификатор пользователя, у которого очищены токены.</returns>
		public Task<ServiceResult<int>> ClearToken(string accessToken);
	}
}
