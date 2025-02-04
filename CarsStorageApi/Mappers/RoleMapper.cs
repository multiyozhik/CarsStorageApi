using AutoMapper;
using CarsStorage.BLL.Abstractions.ModelsDTO.RoleDTO;
using CarsStorageApi.Models.RoleModels;

namespace CarsStorageApi.Mappers
{
	public class RoleMapper: Profile
	{
		public RoleMapper() 
		{
			CreateMap<RoleRequest, RoleCreaterDTO>();

			CreateMap<RoleDTO, RoleRequestResponse>();

			CreateMap<RoleRequestResponse, RoleDTO>();
		}
	}
}
