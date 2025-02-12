using CarsStorage.BLL.Abstractions.General;
using CarsStorage.BLL.Abstractions.ModelsDTO.User;

namespace CarsStorage.BLL.Abstractions.Services
{
	/// <summary>
	/// Интерфейс для сервиса пользователей.
	/// </summary>
	public interface IUsersService
	{
		public Task<ServiceResult<List<UserDTO>>> GetList();
		public Task<ServiceResult<UserDTO>> GetById(int id);
		public Task<ServiceResult<UserDTO>> Create(UserCreaterDTO userCreaterDTO);
		public Task<ServiceResult<UserDTO>> Update(UserUpdaterDTO userUpdaterDTO);
		public Task<ServiceResult<int>> Delete(int id);
	}
}
