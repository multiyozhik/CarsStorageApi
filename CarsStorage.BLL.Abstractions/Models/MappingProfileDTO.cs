using AutoMapper;
using CarsStorage.DAL.Entities;
using CarsStorage.DAL.Models;

namespace CarsStorage.BLL.Abstractions.Models
{
	public class MappingProfileDTO : Profile
	{
		public MappingProfileDTO()
		{
			CreateMap<CarDTO, CarEntity>();

			CreateMap<CarCreaterDTO, CarEntity>();

			CreateMap<CarEntity, CarDTO>();

			CreateMap<AppUserEntity, AppUserDTO>();

			CreateMap<AppUserCreaterDTO, AppUserEntity>();

			CreateMap<AppUserCreaterDTO, IdentityAppUserCreater>();

			CreateMap<RoleEntity, RoleDTO>();

			CreateMap<RoleDTO, RoleEntity>();
		}
	}
}
