using AutoMapper;
using CarsStorage.BLL.Abstractions.ModelsDTO.Role;
using CarsStorage.BLL.Abstractions.ModelsDTO.User;

namespace CarsStorage.BLL.Implementations.MappersBLL
{
	public class UserMapperBLL : Profile
	{
		public UserMapperBLL()
		{
			CreateMap<UserRegisterDTO, UserCreaterDTO>().ForMember(dest => dest.RolesList, opt => opt.MapFrom(src => new List<RoleDTO>()));
		}
	}
}
