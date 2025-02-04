using AutoMapper;
using CarsStorage.BLL.Abstractions.ModelsDTO.AuthModels;
using CarsStorage.BLL.Abstractions.ModelsDTO.UserDTO;
using CarsStorage.DAL.Entities;
using CarsStorage.DAL.Models;

namespace CarsStorage.BLL.Abstractions.Mappers
{
	public class UserMapper : Profile
	{
		public UserMapper()
		{
			CreateMap<UserEntity, UserDTO>();

			CreateMap<UserCreaterDTO, UserEntity>();

			//CreateMap<UserCreaterDTO, UserCreater>();

			CreateMap<UserRegisterDTO, UserRegister>();
		}
	}
}
