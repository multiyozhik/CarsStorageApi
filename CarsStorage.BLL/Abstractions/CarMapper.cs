using Riok.Mapperly.Abstractions;
using CarsStorage.DAL.Entities;
using CarsStorage.BLL.Servises;

namespace CarsStorage.BLL
{
	[Mapper]
	public partial class CarMapper
	{
		public partial CarRow CarToCarRow(Car car);
		public partial Car CarRowToCar(CarRow carRow);
	}
}
