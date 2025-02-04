using AutoMapper;
using CarsStorage.BLL.Abstractions.ModelsDTO.UserDTO;
using CarsStorageApi.Models.UserModels;

namespace CarsStorageApi.Mappers
{
	public class UserMapper: Profile
	{
		public UserMapper() 
		{
			CreateMap<UserRequestResponse, UserRequestResponse>();

			CreateMap<UserRequest, UserCreaterDTO>();
		}	
	}
}
