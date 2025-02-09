using CarsStorage.BLL.Abstractions.ModelsDTO;

namespace CarsStorageApi.Models.RoleModels
{
	/// <summary>
	/// Класс роли пользователя, возвращаемой клиенту.
	/// </summary>
	public class RoleResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<RoleClaimType>? RoleClaims { get; set; }
    }
}
