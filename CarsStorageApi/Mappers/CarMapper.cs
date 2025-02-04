using AutoMapper;
using CarsStorage.BLL.Abstractions.ModelsDTO.CarDTO;
using CarsStorageApi.Models.CarModels;

namespace CarsStorageApi.Mappers
{
	public class CarMapper : Profile
	{
		public CarMapper()
		{
			CreateMap<CarRequestResponse, CarDTO>();

			CreateMap<CarDTO, CarRequestResponse>();

			CreateMap<CarRequest, CarCreaterDTO>();
		}
	}
}
