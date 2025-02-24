namespace CarsStorage.Abstractions.ModelsDTO.Role
{
	/// <summary>
	/// Класс роли, включающий ее идентификатор.
	/// </summary>
	public class RoleDTO
	{
		/// <summary>
		/// Идентификатор роли.
		/// </summary>
		public int Id { get; set; }


		/// <summary>
		/// Наименование роли.
		/// </summary>
		public string Name { get; set; } = string.Empty;


		/// <summary>
		/// Список утверждений для роли.
		/// </summary>
		public List<RoleClaimTypeBLL> RoleClaims { get; set; } = [];
	}
}
