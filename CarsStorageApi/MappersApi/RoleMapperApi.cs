using AutoMapper;
using CarsStorage.BLL.Abstractions.ModelsDTO.Role;
using CarsStorageApi.Models.RoleModels;

namespace CarsStorageApi.MappersApi
{
	/// <summary>
	/// Класс меппера для ролей.
	/// </summary>
	public class RoleMapperApi: Profile
	{
		public RoleMapperApi() 
		{

			CreateMap<RoleDTO, RoleResponse>();

			CreateMap<RoleResponse, RoleDTO>();
		}
	}
}
