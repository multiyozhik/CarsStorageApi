using CarsStorage.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace CarsStorageApi.Models.RoleModels
{
    public class RoleRequestResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public IEnumerable<RoleClaimType> RoleClaims { get; set; }
    }
}
