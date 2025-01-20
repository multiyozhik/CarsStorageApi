using CarsStorage.BLL.Abstractions;
using CarsStorage.BLL.Servises;
using Riok.Mapperly.Abstractions;

namespace CarsStorageApi
{
	[Mapper]
	public partial class CarMapper
	{
		public partial Car CarDtoToCar(CarDTO carDTO);
		public partial CarDTO CarToCarDto(Car car);

		public partial Car CreaterCarDtoToCar(CreaterCarDTO createrCarDTO);
	}
}
