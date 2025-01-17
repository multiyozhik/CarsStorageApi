using CarsStorage.BLL.Servises;
using CarsStorage.DAL.Entities;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsStorage.BLL.Abstractions
{
	[Mapper]
	public partial class CarMapper
	{
		public partial CarRow CarToCarRow(Car car);
		public partial Car CarRowToCar(CarRow carRow);
	}
}
