using AutoMapper;
using CarsStorage.BLL.Abstractions.ModelsDTO.Car;

namespace CarsStorage.BLL.Implementations.MappersBLL
{
	public class CarMapperBLL : Profile
	{
		public CarMapperBLL() 
		{
			CreateMap<CarCreaterDTO, CarDTO>();
		}
	}
}
