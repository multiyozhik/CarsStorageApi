using CarsStorage.BLL.Abstractions.ModelsDTO.Role;
using CarsStorage.BLL.Abstractions.ModelsDTO.User;
using System.Security.Claims;

namespace CarsStorage.BLL.Abstractions.Repositories
{
	/// <summary>
	/// Интерфейс для репозитория ролей пользователя.
	/// </summary>
	public interface IRolesRepository
	{
		public Task<List<RoleDTO>> GetList();
		public Task<List<RoleDTO>> GetRolesByNamesList(IEnumerable<string> roleNamesList);
		public List<Claim> GetClaimsByUser(UserDTO userDTO);
	}
}
