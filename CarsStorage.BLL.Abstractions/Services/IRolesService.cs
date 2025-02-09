using CarsStorage.BLL.Abstractions.General;
using CarsStorage.BLL.Abstractions.ModelsDTO.Role;
using CarsStorage.BLL.Abstractions.ModelsDTO.User;
using System.Security.Claims;

namespace CarsStorage.BLL.Abstractions.Services
{
	/// <summary>
	/// Интерфейс для сервиса ролей.
	/// </summary>
	public interface IRolesService
	{
		public Task<ServiceResult<List<RoleDTO>>> GetList();
		public Task<ServiceResult<List<RoleDTO>>> GetRolesByNamesList(IEnumerable<string> roleNamesList);
		public Task<ServiceResult<List<Claim>>> GetClaimsByUser(UserDTO userDTO);
	}
}
