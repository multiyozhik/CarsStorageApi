using CarsStorage.BLL.Abstractions.Interfaces;
using CarsStorageApi.Mappers;
using CarsStorageApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarsStorageApi.Controllers
{
	[ApiController]
	[Authorize(Policy = "RequierManageRoles")]
	[Route("[controller]/[action]")]

	public class RolesController(IRolesService rolesService) : ControllerBase
	{
		private readonly RolesMapper rolesMapper = new();

		[HttpGet]
		public async Task<IEnumerable<RoleRequestResponse>> GetRoles()
		{
			var rolesDTOList = await rolesService.GetList();
			return rolesDTOList.Select(rolesMapper.RoleDtoToRoleRequestResponse);
		}


		[HttpPost]
		public async Task Create([FromBody] RoleRequest roleRequest)
		{
			await rolesService.Create(rolesMapper.RoleRequestToRoleCreaterDTO(roleRequest));
		}


		[HttpPut]
		public async Task Update([FromBody] RoleRequestResponse roleRequestResponse)
		{
			await rolesService.Update(rolesMapper.RoleRequestResponseToRoleDto(roleRequestResponse));
		}


		[HttpDelete("{id}")]
		public async Task Delete([FromRoute] int id)
		{
			await rolesService.Delete(id);
		}
	}
}
