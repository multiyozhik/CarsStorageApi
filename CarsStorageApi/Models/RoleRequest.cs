using CarsStorage.DAL.Entities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CarsStorageApi.Models
{
	public class RoleRequest
	{
		[Required]
		[StringLength(50)]
		public string? Name { get; set; }

		[Required]
		public IEnumerable<RoleClaimType> RoleClaims { get; set; }		 
	}
}
