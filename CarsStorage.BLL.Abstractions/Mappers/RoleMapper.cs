using AutoMapper;
using CarsStorage.BLL.Abstractions.ModelsDTO.RoleDTO;
using CarsStorage.DAL.Entities;

namespace CarsStorage.BLL.Abstractions.Mappers
{
	public class RoleMapper : Profile
	{
		public RoleMapper()
		{
			CreateMap<RoleEntity, RoleDTO>();

			CreateMap<RoleDTO, RoleEntity>();
		}
	}
}
