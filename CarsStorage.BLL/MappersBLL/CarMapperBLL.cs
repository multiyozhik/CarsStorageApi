using AutoMapper;
using CarsStorage.Abstractions.ModelsDTO.Car;
using CarsStorage.DAL.Entities;

namespace CarsStorage.BLL.Services.MappersBLL
{
	public class CarMapperBLL : Profile
	{
		public CarMapperBLL() 
		{
			//CreateMap<CarCreaterDTO, CarDTO>();

			CreateMap<CarEntity, CarDTO>();

			CreateMap<CarDTO, CarEntity>();

			//CreateMap<CarCreaterDTO, CarEntity>();
		}
	}
}
