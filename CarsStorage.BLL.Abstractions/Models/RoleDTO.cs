using CarsStorage.DAL.Entities;


namespace CarsStorage.BLL.Abstractions.Models
{
	public class RoleDTO
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public IEnumerable<RoleClaimType> RoleClaims { get; set; }
	}
}
