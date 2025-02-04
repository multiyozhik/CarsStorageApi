using CarsStorage.DAL.Models;


namespace CarsStorage.BLL.Abstractions.ModelsDTO.RoleDTO
{
	public class RoleDTO
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public IEnumerable<RoleClaimType> RoleClaims { get; set; }
	}
}
