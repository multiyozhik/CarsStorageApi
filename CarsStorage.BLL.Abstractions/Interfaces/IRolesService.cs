using CarsStorage.BLL.Abstractions.Models;
using CarsStorage.BLL.Abstractions.ModelsDTO.RoleDTO;
using CarsStorage.BLL.Abstractions.ModelsDTO.UserDTO;
using System.Security.Claims;

namespace CarsStorage.BLL.Abstractions.Interfaces
{
	public interface IRolesService
	{
		public Task<ServiceResult<List<RoleDTO>>> GetList();
		public Task<ServiceResult<RoleDTO>> GetRoleById(int id);
		public Task<ServiceResult<List<RoleDTO>>> GetRolesByNamesList(IEnumerable<string> roleNamesList);
		public List<Claim> GetClaimsByUser(UserDTO appUserDTO);
	}
}
