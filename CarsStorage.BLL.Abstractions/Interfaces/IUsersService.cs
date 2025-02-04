using CarsStorage.BLL.Abstractions.Models;
using CarsStorage.BLL.Abstractions.ModelsDTO.UserDTO;

namespace CarsStorage.BLL.Abstractions.Interfaces
{
	/// <summary>
	/// Интерфейс для сервиса пользователей.
	/// </summary>
	public interface IUsersService
	{
		public Task<ServiceResult<List<UserDTO>>> GetList();
		public Task<ServiceResult<UserDTO>> GetById(int id);
		public Task<ServiceResult<UserDTO>> Create(UserCreaterDTO userCreaterDTO);
		public Task<ServiceResult<UserDTO>> Update(UserDTO userDTO);
		public Task<ServiceResult<int>> Delete(int id);
		public Task<ServiceResult<UserDTO>> GetUserByRefreshToken(string refreshToken);
	}
}
