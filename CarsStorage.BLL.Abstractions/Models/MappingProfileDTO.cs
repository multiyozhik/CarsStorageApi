using AutoMapper;
using CarsStorage.DAL.Entities;

namespace CarsStorage.BLL.Abstractions.Models
{
	public class MappingProfileDTO : Profile
	{
		public MappingProfileDTO()
		{
			CreateMap<CarDTO, CarEntity>();

			CreateMap<CarCreaterDTO, CarEntity>();

			CreateMap<CarEntity, CarDTO>();
		}
	}
}
