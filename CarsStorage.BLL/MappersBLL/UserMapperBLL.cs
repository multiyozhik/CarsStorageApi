using AutoMapper;
using CarsStorage.Abstractions.ModelsDTO.User;
using CarsStorage.BLL.Abstractions.ModelsDTO.User;
using CarsStorage.DAL.Entities;

namespace CarsStorage.BLL.Services.MappersBLL
{
	public class UserMapperBLL : Profile
	{
		public UserMapperBLL() 
		{
			CreateMap<UserCreaterDTO, UserDTO>()
				 .ForMember(dest => dest.RolesList, opt => opt.Ignore());

			CreateMap<UserLoginDTO, UserEntity>()
				.ForMember(dest => dest.RolesList, opt => opt.Ignore());

			CreateMap<UserDTO, UserEntity>()
				.ForMember(dest => dest.UserEntityId, opt => opt.MapFrom(src => src.Id));

			CreateMap<UserEntity, UserDTO>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserEntityId));

			CreateMap<UserCreaterDTO, UserEntity>()
				.ForMember(dest => dest.RolesList, opt => opt.Ignore());

			CreateMap<UserUpdaterDTO, UserEntity>()
				.ForMember(dest => dest.RolesList, opt => opt.Ignore())
				.ForMember(dest => dest.UserEntityId, opt => opt.MapFrom(src => src.Id));
		}
	}
}
