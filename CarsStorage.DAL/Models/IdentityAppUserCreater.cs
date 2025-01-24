using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsStorage.DAL.Models
{
	public class IdentityAppUserCreater
	{
		public string? UserName { get; set; }

		public string? Email { get; set; }

		public string? Password { get; set; }

		public IEnumerable<string>? Roles { get; set; }
	}
}
