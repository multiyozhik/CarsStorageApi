﻿using AutoMapper;
using CarsStorage.Abstractions.ModelsDTO.Role;
using CarsStorage.DAL.Entities;

namespace CarsStorage.BLL.Services.MappersBLL
{
	/// <summary>
	/// Класс меппера для объектов ролей.
	/// </summary>
	public class RoleMapperBLL : Profile
	{
		/// <summary>
		/// Конструктор меппера для объектов ролей.
		/// </summary>
		public RoleMapperBLL()
		{
			CreateMap<RoleEntity, RoleDTO>()
				.ForMember(dist => dist.Id, opt => opt.MapFrom(src => src.RoleEntityId));

			CreateMap<RoleDTO, RoleEntity>()
				.ForMember(dist => dist.RoleEntityId, opt => opt.MapFrom(src => src.Id));
		}
	}
}
