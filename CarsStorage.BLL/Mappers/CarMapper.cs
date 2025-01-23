using CarsStorage.BLL.Abstractions.Models;
using CarsStorage.DAL.Entities;
using Riok.Mapperly.Abstractions;

namespace CarsStorage.BLL.Implementations.Mappers
{
    [Mapper]
	public partial class CarMapper
	{
		public partial CarEntity CarToCarEntity(CarDTO car);
		public partial CarDTO CarEntityToCarDto(CarEntity carRow);
	}
}
