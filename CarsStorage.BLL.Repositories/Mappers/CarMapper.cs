using AutoMapper;
using CarsStorage.Abstractions.ModelsDTO.Car;
using CarsStorage.DAL.Entities;

namespace CarsStorage.DAL.Repositories.Mappers
{
	/// <summary>
	/// Класс меппера для автомобилей.
	/// </summary>
	public class CarMapper : Profile
	{
		public CarMapper()
		{
			CreateMap<CarEntity, CarDTO>();

			CreateMap<CarDTO, CarEntity>();

			CreateMap<CarCreaterDTO, CarEntity>();
		}
	}
}
