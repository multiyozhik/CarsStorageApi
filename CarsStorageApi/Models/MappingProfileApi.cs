using AutoMapper;
using CarsStorage.BLL.Abstractions.Models;
using CarsStorageApi.AuthModels;

namespace CarsStorageApi.Models
{
	public class MappingProfileApi : Profile
	{
		public MappingProfileApi()
		{
			CreateMap<CarRequestResponse, CarDTO>();

			CreateMap<CarDTO, CarRequestResponse>();

			CreateMap<CarRequest, CarCreaterDTO>();

			CreateMap<JWTTokenDTO, JWTTokenRequestResponse>();

			CreateMap<UserRequestResponse, UserRequestResponse>();

			CreateMap<RegisterUserDataRequest, AppUserRegisterDTO>();

			CreateMap<LoginDataRequest, AppUserLoginDTO>();

			CreateMap<UserRequest, AppUserCreaterDTO>();

			CreateMap<RoleRequest, RoleCreaterDTO>();

			CreateMap<RoleDTO, RoleRequestResponse>();

			CreateMap<RoleRequestResponse, RoleDTO>();
		}
	}
}
