using CarsStorage.BLL.Abstractions.Models;
using CarsStorageApi.Models;
using Riok.Mapperly.Abstractions;

namespace CarsStorageApi.Mappers
{
    [Mapper]
	public partial class CarMapper
	{
		public partial CarDTO CarRequestResponseToCarDTO(CarRequestResponse carRequestResponse);
		public partial CarRequestResponse CarDtoToCarRequestResponse(CarDTO carDTO);

		public partial CarCreaterDTO CarRequestToCarCreaterDTO(CarRequest carRequest);
	}
}
