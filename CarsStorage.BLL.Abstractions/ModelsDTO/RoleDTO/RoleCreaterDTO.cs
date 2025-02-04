using CarsStorage.DAL.Models;

namespace CarsStorage.BLL.Abstractions.ModelsDTO.RoleDTO
{
	/// <summary>
	/// Класс для создания роли по имени роли и списку клаймов.
	/// </summary>
	public class RoleCreaterDTO
	{
		public string? Name { get; set; }
		public IEnumerable<RoleClaimType>? RoleClaims { get; set; }
	}
}
