using AutoMapper;
using CarsStorage.BLL.Abstractions.ModelsDTO.User;
using CarsStorage.DAL.Entities;

namespace CarsStorage.BLL.Repositories.Mappers
{
	/// <summary>
	/// Класс меппера для пользователей.
	/// </summary>
	public class UserMapper : Profile
	{
		public UserMapper()
		{

			CreateMap<UserLoginDTO, UserEntity>();

			CreateMap<UserDTO, UserEntity>()
				.ForMember(dest => dest.UserEntityId, opt => opt.MapFrom(src => src.Id));

			CreateMap<UserEntity, UserDTO>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserEntityId));

			CreateMap<UserCreaterDTO, UserEntity>();

			CreateMap<UserUpdaterDTO, UserEntity>();
		}
	}
}
