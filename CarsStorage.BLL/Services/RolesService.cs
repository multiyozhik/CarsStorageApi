using AutoMapper;
using CarsStorage.BLL.Abstractions.Interfaces;
using CarsStorage.BLL.Abstractions.Models;
using CarsStorage.BLL.Repositories.Interfaces;
using CarsStorage.DAL.Entities;

namespace CarsStorage.BLL.Implementations.Services
{
	public class RolesService(IRoleRepository rolesRepository, IMapper mapper) : IRolesService
	{
		public async Task<ServiceResult<IEnumerable<RoleDTO>>> GetList()
		{
			try
			{
				var roleEntityList = await rolesRepository.GetList();
				var rolesDTOList = roleEntityList.Select(mapper.Map<RoleDTO>);
				return new ServiceResult<IEnumerable<RoleDTO>>(rolesDTOList, null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<IEnumerable<RoleDTO>>(null, exception.Message);
			}
		}

		public async Task<ServiceResult<RoleDTO>> GetRoleById(int id)
		{
			try
			{
				var roleEntity = await rolesRepository.GetRoleById(id);
				return new ServiceResult<RoleDTO>(mapper.Map<RoleDTO>(roleEntity), null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<RoleDTO>(null, exception.Message);
			}
		}

		public async Task<ServiceResult<IEnumerable<RoleDTO>>> GetRolesByNamesList(IEnumerable<string> roleNamesList)
		{
			try
			{
				var roleEntityList = await rolesRepository.GetRolesByNamesList(roleNamesList);
				return new ServiceResult<IEnumerable<RoleDTO>>(roleEntityList.Select(mapper.Map<RoleDTO>), null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<IEnumerable<RoleDTO>>(null, exception.Message);
			}
		}


	}
}
