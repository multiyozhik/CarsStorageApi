using AutoMapper;
using CarsStorage.BLL.Abstractions.ModelsDTO.User;

namespace CarsStorage.BLL.Implementations.MappersBLL
{
	public class UserMapperBLL : Profile
	{
		public UserMapperBLL() 
		{
			CreateMap<UserCreaterDTO, UserDTO>()
				 .ForMember(dest => dest.RolesList, opt => opt.Ignore());
		}
	}
}
