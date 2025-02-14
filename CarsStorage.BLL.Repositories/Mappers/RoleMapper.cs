using AutoMapper;
using CarsStorage.Abstractions.ModelsDTO.Role;
using CarsStorage.DAL.Entities;

namespace CarsStorage.DAL.Repositories.Mappers
{
	/// <summary>
	/// Класс меппера для ролей.
	/// </summary>
	public class RoleMapper : Profile
	{
		public RoleMapper()
		{
			CreateMap<RoleEntity, RoleDTO>()
				.ForMember(dist => dist.Id, opt => opt.MapFrom(src => src.RoleEntityId));

			CreateMap<RoleDTO, RoleEntity>()
				.ForMember(dist => dist.RoleEntityId, opt => opt.MapFrom(src => src.Id));
		}
	}
}
