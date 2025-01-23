using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsStorage.BLL.Abstractions.Models
{
	public class CarCreaterDTO
	{
		public string? Model { get; set; }
		public string? Make { get; set; }
		public string? Color { get; set; }
		public int Count { get; set; }
	}
}
