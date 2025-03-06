using AutoMapper;
using CarsStorage.Abstractions.ModelsDTO.Location;
using CarsStorageApi.Models.LocationModels;

namespace CarsStorageApi.MappersApi
{
	/// <summary>
	/// Класс меппера для объектов для локализации пользователя.
	/// </summary>
	public class LocationMapperApi: Profile
	{
		/// <summary>
		/// Конструктор меппера объектов для локализации пользователя.
		/// </summary>
		public LocationMapperApi()
		{
			CreateMap<CoordinateRequest, CoordinateDTO>();

			CreateMap<LocationDTO, LocationResponse>();
		}
	}
}
