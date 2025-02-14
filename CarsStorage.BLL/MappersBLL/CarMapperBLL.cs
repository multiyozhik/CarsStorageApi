using AutoMapper;
using CarsStorage.Abstractions.ModelsDTO.Car;

namespace CarsStorage.BLL.Services.MappersBLL
{
	public class CarMapperBLL : Profile
	{
		public CarMapperBLL() 
		{
			CreateMap<CarCreaterDTO, CarDTO>();
		}
	}
}
