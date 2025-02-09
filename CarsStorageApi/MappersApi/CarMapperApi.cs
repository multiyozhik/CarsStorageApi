using AutoMapper;
using CarsStorage.BLL.Abstractions.ModelsDTO.Car;
using CarsStorageApi.Models.CarModels;

namespace CarsStorageApi.MappersApi
{
	/// <summary>
	/// Класс меппера для автомобилей.
	/// </summary>
	public class CarMapperApi : Profile
	{
		public CarMapperApi()
		{
			CreateMap<CarResponse, CarDTO>();

			CreateMap<CarDTO, CarResponse>();

			CreateMap<CarRequest, CarCreaterDTO>();
		}
	}
}
