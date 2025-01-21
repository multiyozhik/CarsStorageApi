using CarsStorage.BLL.Abstractions;
using CarsStorage.DAL.Entities;
using Riok.Mapperly.Abstractions;

namespace CarsStorage.BLL.Mappers
{
	[Mapper]
	public partial class CarMapper
	{
		public partial CarEntity CarToCarRow(Car car);
		public partial Car CarRowToCar(CarEntity carRow);
	}
}
