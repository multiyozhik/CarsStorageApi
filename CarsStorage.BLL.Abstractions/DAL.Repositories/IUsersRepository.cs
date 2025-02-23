using CarsStorage.DAL.Entities;

namespace CarsStorage.Abstractions.DAL.Repositories
{
	/// <summary>
	/// Интерфейс для репозитория пользователей.
	/// </summary>
	public interface IUsersRepository
	{
		public Task<List<UserEntity>> GetList();
		public Task<UserEntity> GetUserByUserId(int id);
		public Task<UserEntity?> GetUserByUserName(string userName);
		public Task<List<RoleEntity>> GetRolesByRoleNames(List<string> roleNamesList);
		public Task<UserEntity> GetUserByRefreshToken(string refreshToken);
		public Task<UserEntity> Create(UserEntity userEntity);
		public Task<UserEntity> Update(UserEntity userEntity);
		public Task Delete(int id);
	}
}
