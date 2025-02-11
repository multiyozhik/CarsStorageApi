using AutoMapper;
using CarsStorage.BLL.Abstractions.ModelsDTO.Car;
using CarsStorage.DAL.Entities;
using System.Globalization;
using System.Reflection;

namespace CarsStorage.BLL.Repositories.Mappers
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
