using AutoMapper;
using CarsStorage.BLL.Abstractions.ModelsDTO.AuthModels;
using CarsStorage.BLL.Abstractions.ModelsDTO.UserDTO;
using CarsStorage.DAL.Entities;
using CarsStorage.DAL.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarsStorage.BLL.Abstractions.Mappers
{
	/// <summary>
	/// Класс меппера для пользователей.
	/// </summary>
	public class UserMapper : Profile
	{
		public UserMapper()
		{
			CreateMap<UserRegister, UserCreaterWithRolesDTO>();

			CreateMap<UserRegisterDTO, UserCreater>();

			CreateMap<UserRegister, UserCreaterWithRolesDTO>();

			CreateMap<UserLoginDTO, UserEntity>();

			CreateMap<UserDTO, UserEntity>();

			CreateMap<UserEntity, UserDTO>();

			CreateMap<UserCreaterDTO, UserCreater>();

			CreateMap<EntityEntry<UserEntity>, UserRegister>();
		}
	}
}
