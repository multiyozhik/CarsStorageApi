using AutoMapper;
using CarsStorage.BLL.Abstractions.ModelsDTO.AuthModels;
using CarsStorage.BLL.Abstractions.ModelsDTO.UserDTO;
using CarsStorage.DAL.Entities;
using CarsStorageApi.Models.AuthModels;
using CarsStorageApi.Models.UserModels;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.RegularExpressions;

namespace CarsStorageApi.Mappers
{
	/// <summary>
	/// Класс меппера для пользователей.
	/// </summary>
	public class UserMapperApi: Profile
	{
		public UserMapperApi() 
		{
			CreateMap<RegisterUserDataRequest, UserRequestResponse>();

			CreateMap<UserCreaterWithRolesDTO, UserRequestResponse>();

			CreateMap<LoginDataRequest, UserLoginDTO>();

			CreateMap<UserDTO, UserRequestResponse>();

			CreateMap<UserRequest, UserCreaterDTO>();

			CreateMap<RegisterUserDataRequest, UserRegisterDTO>();
		}	
	}
}
