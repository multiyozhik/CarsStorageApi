using CarsStorage.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsStorage.BLL.Abstractions.Models
{
	public class RoleCreaterDTO
	{
		public string? Name { get; set; }
		public IEnumerable<RoleClaimType> RoleClaims { get; set; }
	}
}
