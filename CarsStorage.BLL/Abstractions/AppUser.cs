using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsStorage.BLL.Abstractions
{
	public class AppUser
	{
		public Guid Id { get; set; }
		public string? Username { get; set; }
		public string? Password { get; set; }
	}
}
