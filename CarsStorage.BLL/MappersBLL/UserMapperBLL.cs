using AutoMapper;
using CarsStorage.Abstractions.ModelsDTO.User;
using CarsStorage.BLL.Abstractions.ModelsDTO.User;

namespace CarsStorage.BLL.Services.MappersBLL
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
