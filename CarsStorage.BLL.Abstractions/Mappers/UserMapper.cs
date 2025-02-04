using AutoMapper;
using CarsStorage.BLL.Abstractions.ModelsDTO.AuthModels;
using CarsStorage.BLL.Abstractions.ModelsDTO.UserDTO;
using CarsStorage.DAL.Entities;
using CarsStorage.DAL.Models;

namespace CarsStorage.BLL.Abstractions.Mappers
{
	/// <summary>
	/// Класс меппера для пользователей.
	/// </summary>
	public class UserMapper : Profile
	{
		public UserMapper()
		{
			CreateMap<UserEntity, UserDTO>();

			CreateMap<UserCreaterDTO, UserEntity>();

			CreateMap<UserRegisterDTO, UserRegister>();
		}
	}
}
