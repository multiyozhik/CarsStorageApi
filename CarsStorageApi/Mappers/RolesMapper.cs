using CarsStorage.BLL.Abstractions.Models;
using CarsStorageApi.Models;
using Riok.Mapperly.Abstractions;

namespace CarsStorageApi.Mappers
{
	[Mapper]
	public partial class RolesMapper
	{
		public partial RoleCreaterDTO RoleRequestToRoleCreaterDTO(RoleRequest roleRequest);
		public partial RoleRequestResponse RoleDtoToRoleRequestResponse(RoleDTO roleDTO);
		public partial RoleDTO RoleRequestResponseToRoleDto(RoleRequestResponse roleRequestResponse);
	}
}
