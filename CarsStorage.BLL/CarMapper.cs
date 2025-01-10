using Riok.Mapperly.Abstractions;
using CarsStorage.DAL.Entities;

namespace CarsStorage.BLL
{
	[Mapper]
	public partial class CarMapper
	{
		public partial CarRow CarDTOToCarRow(CarDTO carDTO);
		public partial CarDTO CarRowToCarDTO(CarRow carModel);
	}
}
