using AutoMapper;
using CarsStorage.BLL.Abstractions.ModelsDTO.CarDTO;
using CarsStorage.DAL.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarsStorage.BLL.Abstractions.Mappers
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

			CreateMap<EntityEntry<CarEntity>, CarEntity>();
		}
	}
}
