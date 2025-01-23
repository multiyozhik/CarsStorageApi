using CarsStorage.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace CarsStorageApi.Models
{
	public class RoleRequestResponse
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public IEnumerable<RoleClaimType> RoleClaims { get; set; }
	}
}
