using CarsStorage.Abstractions.ModelsDTO.User;
using CarsStorage.BLL.Abstractions.ModelsDTO.User;

namespace CarsStorage.Abstractions.DAL.Repositories
{
	/// <summary>
	/// Интерфейс для репозитория пользователей.
	/// </summary>
	public interface IUsersRepository
	{
		public Task<List<UserDTO>> GetList();
		public Task<UserDTO> GetById(int userId);
		public Task IsUserValid(UserLoginDTO userLoginDTO);
		public Task<UserDTO> GetUserWithRoles(UserLoginDTO userLoginDTO);
		public Task<UserDTO> Create(UserCreaterDTO userCreaterDTO);
		public Task<UserDTO> Update(UserUpdaterDTO userUpdaterDTO);
		public Task Delete(int id);
		public Task<UserDTO> GetUserByRefreshToken(string refreshToken);
		public Task<UserDTO> GetUserByAuthUserData(AuthUserData authUserData);
	}
}
