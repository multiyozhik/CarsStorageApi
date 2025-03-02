using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsStorage.DAL.Entities
{
	public class TechnicalWork
	{
		public string Name { get; set; }
		public DateTime StartTimestamp { get; set; }
		public DateTime EndTimestamp { get; set; }
	}
}
