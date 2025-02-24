using CarsStorage.DAL.Entities;

namespace CarsStorage.Abstractions.DAL.Repositories
{
	/// <summary>
	/// Интерфейс для репозитория пользователей.
	/// </summary>
	public interface IUsersRepository
	{
		/// <summary>
		/// Метод для получения списка объектов пользователей.
		/// </summary>
		/// <returns>Список объектов пользователей.</returns>
		public Task<List<UserEntity>> GetList();


		/// <summary>
		/// Метод для получения объекта пользователя по его идентификатору.
		/// </summary>
		/// <param name="id">Идентификатор пользователя.</param>
		/// <returns>Объект пользователя.</returns>
		public Task<UserEntity> GetUserByUserId(int id);


		/// <summary>
		/// Метод для получения объекта пользователя по его имени.
		/// </summary>
		/// <param name="userName">Имя пользователя.</param>
		/// <returns>Объект пользователя.</returns>
		public Task<UserEntity?> GetUserByUserName(string userName);


		/// <summary>
		/// Метод для получения списка объектов ролей по списку наименований этих ролей.
		/// </summary>
		/// <param name="roleNamesList">Список наименований ролей.</param>
		/// <returns></returns>
		public Task<List<RoleEntity>> GetRolesByRoleNames(List<string> roleNamesList);


		/// <summary>
		/// Метод для получения объекта пользователя по его токену обновления.
		/// </summary>
		/// <param name="refreshToken">Токен обновления для пользователя.</param>
		/// <returns>Объект пользователя.</returns>
		public Task<UserEntity> GetUserByRefreshToken(string refreshToken);


		/// <summary>
		/// Метод для создания объекта пользователя.
		/// </summary>
		/// <param name="userEntity">Объект пользователя для создания.</param>
		/// <returns>Созданный объект пользователя.</returns>
		public Task<UserEntity> Create(UserEntity userEntity);


		/// <summary>
		/// Метод для изменения объекта пользователя.
		/// </summary>
		/// <param name="userEntity">Объект пользователя для изменения.</param>
		/// <returns>Измененный объект пользователя.</returns>
		public Task<UserEntity> Update(UserEntity userEntity);


		/// <summary>
		/// Метод для удаления объекта пользователя по его идентификатору.
		/// </summary>
		/// <param name="id">Идентификатор пользователя.</param>
		/// <returns>Идентификатор удаленного пользователя.</returns>
		public Task Delete(int id);
	}
}
