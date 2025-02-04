using AutoMapper;
using CarsStorage.BLL.Abstractions.ModelsDTO.RoleDTO;
using CarsStorageApi.Models.RoleModels;

namespace CarsStorageApi.Mappers
{
	/// <summary>
	/// Класс меппера для ролей.
	/// </summary>
	public class RoleMapperApi: Profile
	{
		public RoleMapperApi() 
		{
			CreateMap<RoleRequest, RoleCreaterDTO>();

			CreateMap<RoleDTO, RoleRequestResponse>();

			CreateMap<RoleRequestResponse, RoleDTO>();
		}
	}
}
