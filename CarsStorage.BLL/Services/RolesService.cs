using AutoMapper;
using CarsStorage.BLL.Abstractions.Exceptions;
using CarsStorage.BLL.Abstractions.General;
using CarsStorage.BLL.Abstractions.Services;
using CarsStorage.BLL.Abstractions.ModelsDTO.Role;
using CarsStorage.BLL.Abstractions.ModelsDTO.User;
using System.Security.Claims;
using CarsStorage.BLL.Abstractions.Repositories;

namespace CarsStorage.BLL.Implementations.Services
{
	/// <summary>
	/// Класс сервиса ролей пользователей.
	/// </summary>
	public class RolesService(IRolesRepository rolesRepository, IMapper mapper) : IRolesService
	{
		/// <summary>
		/// Метод возвращает как результат список всех ролей.
		/// </summary>
		public async Task<ServiceResult<List<RoleDTO>>> GetList()
		{
			try
			{
				var rolesDTOList = await rolesRepository.GetList();
				return new ServiceResult<List<RoleDTO>>(rolesDTOList, null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<List<RoleDTO>>(null, new NotFoundException(exception.Message));
			}
		}


		/// <summary>
		/// Метод возвращает как результат список объектов ролей по полученному списку имен ролей.
		/// </summary>
		public async Task<ServiceResult<List<RoleDTO>>> GetRolesByNamesList(IEnumerable<string> roleNamesList)
		{
			try
			{
				var roleDTOList = await rolesRepository.GetRolesByNamesList(roleNamesList);
				return new ServiceResult<List<RoleDTO>>(roleDTOList, null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<List<RoleDTO>>(null, new BadRequestException(exception.Message));
			}
		}


		/// <summary>
		/// Метод возвращает как результат список клаймов для пользователя.
		/// </summary>
		public async Task<ServiceResult<List<Claim>>> GetClaimsByUser(UserDTO userDTO)
		{
			try
			{
				var claimsList = rolesRepository.GetClaimsByUser(userDTO).ToList();
				return new ServiceResult<List<Claim>>(claimsList, null);
			}
			catch(Exception exception)
			{
				return new ServiceResult<List<Claim>>(null, new BadRequestException(exception.Message));
			}
		}
	}
}
