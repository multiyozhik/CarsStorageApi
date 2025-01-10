using CarsStorage.BLL;
using Riok.Mapperly.Abstractions;

namespace CarsStorageApi
{
	[Mapper]
	public partial class CarMapper
	{
		public partial Car CarDTOToCar(CarDTO carDTO);
		public partial CarDTO CarToCarDTO(Car car);
	}
}
