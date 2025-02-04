using AutoMapper;
using CarsStorage.BLL.Abstractions.ModelsDTO.CarDTO;
using CarsStorage.DAL.Entities;

namespace CarsStorage.BLL.Abstractions.Mappers
{
	/// <summary>
	/// Класс меппера для автомобилей.
	/// </summary>
	public class CarMapper : Profile
	{
		public CarMapper()
		{
			CreateMap<CarDTO, CarEntity>();

			CreateMap<CarCreaterDTO, CarEntity>();

			CreateMap<CarEntity, CarDTO>();
		}
	}
}
