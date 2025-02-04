using CarsStorage.DAL.Entities;
using System.Security.Claims;

namespace CarsStorage.BLL.Repositories.Interfaces
{
	/// <summary>
	/// Интерфейс для репозитория ролей пользователя.
	/// </summary>
	public interface IRolesRepository
	{
		public Task<List<RoleEntity>> GetList();
		public Task<RoleEntity> GetRoleById(int id);
		public Task<List<RoleEntity>> GetRolesByNamesList(IEnumerable<string> roleNamesList);
		public List<Claim> GetClaimsByUser(UserEntity identityAppUser);
	}
}
