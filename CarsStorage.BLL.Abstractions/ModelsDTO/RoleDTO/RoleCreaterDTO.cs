using CarsStorage.DAL.Models;

namespace CarsStorage.BLL.Abstractions.ModelsDTO.RoleDTO
{
	public class RoleCreaterDTO
	{
		public string? Name { get; set; }
		public IEnumerable<RoleClaimType>? RoleClaims { get; set; }
	}
}
