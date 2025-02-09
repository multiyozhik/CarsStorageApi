using AutoMapper;
using CarsStorage.BLL.Abstractions.ModelsDTO.Role;
using CarsStorage.BLL.Abstractions.ModelsDTO.User;
using CarsStorageApi.Models.UserModels;

namespace CarsStorageApi.MappersApi
{
	/// <summary>
	/// Класс меппера для пользователей.
	/// </summary>
	public class UserMapperApi: Profile
	{
		public UserMapperApi() 
		{
			CreateMap<RegisterUserRequest, UserRegisterDTO>();

			CreateMap<LoginUserRequest, UserLoginDTO>();

			CreateMap<UserRequest, UserCreaterDTO>()
				.ForMember(dest => dest.RolesList, opt => opt.MapFrom(src => new List<RoleDTO>()));

			CreateMap<UserDTO, UserResponse>()
				.ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.RolesList.Select(role => role.Name).ToList()));
		}	
	}
}
