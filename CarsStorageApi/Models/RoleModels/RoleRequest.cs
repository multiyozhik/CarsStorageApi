using CarsStorage.BLL.Abstractions.ModelsDTO;
using System.ComponentModel.DataAnnotations;

namespace CarsStorageApi.Models.RoleModels
{
	/// <summary>
	/// Класс данных роли пользователя, передаваемых клиентом для ее создания.
	/// </summary>
	public class RoleRequest
    {
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }

        [Required]
        public List<RoleClaimType>? RoleClaims { get; set; }
    }
}
