using CarsStorage.BLL.Abstractions.ModelsDTO.Token;
using CarsStorage.BLL.Abstractions.ModelsDTO.User;

namespace CarsStorage.BLL.Abstractions.Repositories
{
	/// <summary>
	/// Интерфейс для репозитория пользователей.
	/// </summary>
	public interface IUsersRepository
	{
		public Task<List<UserDTO>> GetList();
		public Task<UserDTO> GetById(int userId);
		public Task<UserDTO> GetUserIfValid(UserLoginDTO userLoginDTO);
		public Task<UserDTO> Create(UserCreaterDTO userCreaterDTO);
		public Task<UserDTO> Update(UserDTO userDTO);
		public Task Delete(int id);
		public Task UpdateToken(int id, JWTTokenDTO jwtTokenDTO);
		public Task<UserDTO> GetUserByRefreshToken(string refreshToken);
		public Task<JWTTokenDTO> GetTokenByUserId(int userId);
		public Task<int> ClearToken(string refreshToken);
	}
}
