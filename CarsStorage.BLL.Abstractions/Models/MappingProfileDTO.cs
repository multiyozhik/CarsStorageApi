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

			CreateMap<IdentityAppUser, AppUserDTO>();

			CreateMap<AppUserCreaterDTO, IdentityAppUser>();

			CreateMap<AppUserCreaterDTO, IdentityAppUserCreater>();

			CreateMap<RoleEntity, RoleDTO>();

			CreateMap<RoleDTO, RoleEntity>();
		}
	}
}
