using CarsStorage.DAL.Models;


namespace CarsStorage.BLL.Abstractions.ModelsDTO.RoleDTO
{
	/// <summary>
	/// Класс для роли с id.
	/// </summary>
	public class RoleDTO
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public IEnumerable<RoleClaimType> RoleClaims { get; set; }
	}
}
