using CarsStorage.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CarsStorageApi.Models.RoleModels
{
    /// <summary>
    /// Класс роли для ее создания пользователем (без id).
    /// </summary>
    public class RoleRequest
    {
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }

        [Required]
        public IEnumerable<RoleClaimType> RoleClaims { get; set; }
    }
}
