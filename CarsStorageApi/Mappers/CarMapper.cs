using CarsStorage.BLL.Abstractions;
using Riok.Mapperly.Abstractions;

namespace CarsStorageApi.Mappers
{
	[Mapper]
	public partial class CarMapper
	{
		public partial Car CarDtoToCar(CarDTO carDTO);
		public partial CarDTO CarToCarDto(Car car);
	}
}
