using AutoMapper;
using CarsStorage.Abstractions.ModelsDTO.Car;
using CarsStorageApi.Models.CarModels;

namespace CarsStorageApi.MappersApi
{
	/// <summary>
	/// Класс меппера для объектов автомобилей.
	/// </summary>
	public class CarMapperApi : Profile
	{
		/// <summary>
		/// Конструктор меппера для объектов автомобилей.
		/// </summary>
		public CarMapperApi()
		{
			CreateMap<CarResponse, CarDTO>();

			CreateMap<CarDTO, CarResponse>();

			CreateMap<CarRequest, CarCreaterDTO>();
		}
	}
}
