using AutoMapper;
using CarsStorage.BLL.Abstractions.Interfaces;
using CarsStorage.BLL.Abstractions.Models;
using CarsStorage.BLL.Repositories.Interfaces;
using CarsStorage.DAL.Entities;
using System.Security.Claims;

namespace CarsStorage.BLL.Implementations.Services
{
	public class RolesService(IRolesRepository rolesRepository, IMapper mapper) : IRolesService
	{
		public async Task<ServiceResult<List<RoleDTO>>> GetList()
		{
			try
			{
				var roleEntityList = await rolesRepository.GetList();
				var rolesDTOList = roleEntityList.Select(mapper.Map<RoleDTO>).ToList();
				return new ServiceResult<List<RoleDTO>>(rolesDTOList, null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<List<RoleDTO>>(null, exception.Message);
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

		public async Task<ServiceResult<List<RoleDTO>>> GetRolesByNamesList(IEnumerable<string> roleNamesList)
		{
			try
			{
				var roleEntityList = await rolesRepository.GetRolesByNamesList(roleNamesList);
				var roleDtoList = roleEntityList.Select(mapper.Map<RoleDTO>).ToList();
				return new ServiceResult<List<RoleDTO>>(roleDtoList, null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<List<RoleDTO>>(null, exception.Message);
			}
		}

		public List<Claim> GetClaimsByUser(AppUserDTO appUserDTO)
		{
			return rolesRepository.GetClaimsByUser(mapper.Map<AppUserEntity>(appUserDTO)).ToList();
		}
	}
}
