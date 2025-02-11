namespace CarsStorage.BLL.Abstractions.ModelsDTO.Role
{
	/// <summary>
	/// Класс для роли с id.
	/// </summary>
	public class RoleDTO
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public List<RoleClaimTypeBLL>? RoleClaims { get; set; }
	}
}
