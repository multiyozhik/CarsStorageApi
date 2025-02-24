using CarsStorage.Abstractions.General;
using CarsStorage.Abstractions.ModelsDTO.User;
using CarsStorage.BLL.Abstractions.ModelsDTO.User;

namespace CarsStorage.Abstractions.BLL.Services
{
	/// <summary>
	/// Интерфейс для сервиса пользователей.
	/// </summary>
	public interface IUsersService
	{
		/// <summary>
		/// Метод для получения списка объектов пользователей.
		/// </summary>
		/// <returns>Список объектов пользователей.</returns>
		public Task<ServiceResult<List<UserDTO>>> GetList();


		/// <summary>
		/// Метод для получения объекта пользователя по его идентификатору.
		/// </summary>
		/// <param name="id">Идентификатор пользователя.</param>
		/// <returns>Объект пользователя.</returns>
		public Task<ServiceResult<UserDTO>> GetById(int id);


		/// <summary>
		/// Метод для создания объекта пользователя.
		/// </summary>
		/// <param name="userCreaterDTO">Объект пользователя для создания.</param>
		/// <returns>Созданный объект пользователя.</returns>
		public Task<ServiceResult<UserDTO>> Create(UserCreaterDTO userCreaterDTO);


		/// <summary>
		/// Метод для изменения объекта пользователя.
		/// </summary>
		/// <param name="userUpdaterDTO">Объект пользователя для изменения.</param>
		/// <returns>Измененный объект пользователя.</returns>
		public Task<ServiceResult<UserDTO>> Update(UserUpdaterDTO userUpdaterDTO);


		/// <summary>
		/// Метод для удаления объекта пользователя по его идентификатору.
		/// </summary>
		/// <param name="id">Идентификатор пользователя.</param>
		/// <returns>Идентификатор удаленного пользователя.</returns>
		public Task<ServiceResult<int>> Delete(int id);
	}
}
