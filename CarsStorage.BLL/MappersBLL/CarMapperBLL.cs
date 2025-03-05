using AutoMapper;
using CarsStorage.Abstractions.ModelsDTO.Car;
using CarsStorage.DAL.Entities;

namespace CarsStorage.BLL.Services.MappersBLL
{
	/// <summary>
	/// Класс меппера объектов автомобилей.
	/// </summary>
	public class CarMapperBLL : Profile
	{
		/// <summary>
		/// Конструктор меппера.
		/// </summary>
		public CarMapperBLL() 
		{
			CreateMap<CarEntity, CarDTO>();

			CreateMap<CarDTO, CarEntity>();

			CreateMap<CarCreaterDTO, CarEntity>();
		}
	}
}
