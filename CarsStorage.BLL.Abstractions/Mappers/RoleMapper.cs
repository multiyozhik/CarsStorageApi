using AutoMapper;
using CarsStorage.BLL.Abstractions.ModelsDTO.RoleDTO;
using CarsStorage.DAL.Entities;

namespace CarsStorage.BLL.Abstractions.Mappers
{
	/// <summary>
	/// Класс меппера для ролей.
	/// </summary>
	public class RoleMapper : Profile
	{
		public RoleMapper()
		{
			CreateMap<RoleEntity, RoleDTO>();

			CreateMap<RoleDTO, RoleEntity>();
		}
	}
}
