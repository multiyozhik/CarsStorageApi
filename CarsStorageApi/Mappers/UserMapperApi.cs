using AutoMapper;
using CarsStorage.BLL.Abstractions.ModelsDTO.UserDTO;
using CarsStorageApi.Models.UserModels;

namespace CarsStorageApi.Mappers
{
	/// <summary>
	/// Класс меппера для пользователей.
	/// </summary>
	public class UserMapperApi: Profile
	{
		public UserMapperApi() 
		{
			CreateMap<UserRequestResponse, UserRequestResponse>();

			CreateMap<UserRequest, UserCreaterDTO>();
		}	
	}
}
