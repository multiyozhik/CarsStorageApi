﻿using AutoMapper;
using CarsStorage.Abstractions.ModelsDTO.User;
using CarsStorage.BLL.Abstractions.ModelsDTO.User;
using CarsStorageApi.Models.UserModels;

namespace CarsStorageApi.MappersApi
{
	/// <summary>
	/// Класс меппера для объектов пользователей.
	/// </summary>
	public class UserMapperApi: Profile
	{
		/// <summary>
		/// Конструктор меппера для объектов пользователей.
		/// </summary>
		public UserMapperApi() 
		{
			CreateMap<RegisterUserRequest, UserCreaterDTO>()
				.ForMember(dest => dest.RoleNamesList, opt => opt.MapFrom(src => new List<string>()));

			CreateMap<LoginUserRequest, UserLoginDTO>();

			CreateMap<UserRequest, UserCreaterDTO>();

			CreateMap<UserDTO, UserResponse>()
				.ForMember(dest => dest.RoleNamesList, opt => opt.MapFrom(src => src.RolesList.Select(role => role.Name).ToList()));

			CreateMap<UserResponse, UserUpdaterDTO>();
		}	
	}
}
