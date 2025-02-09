using AutoMapper;
using CarsStorage.BLL.Abstractions.ModelsDTO.Role;
using CarsStorage.BLL.Abstractions.ModelsDTO.User;
using CarsStorage.BLL.Abstractions.Repositories;
using CarsStorage.DAL.DbContexts;
using CarsStorage.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CarsStorage.BLL.Repositories.Implementations
{
	/// <summary>
	/// Класс репозитория для ролей пользователя.
	/// </summary>
	public class RolesRepository(AppDbContext dbContext, IMapper mapper) : IRolesRepository
	{
		/// <summary>
		/// Метод получения всего списка возможных ролей из БД.
		/// </summary>
		public async Task<List<RoleDTO>> GetList()
		{
			var roleEntityList = await dbContext.Roles.ToListAsync();
			return roleEntityList.Select(mapper.Map<RoleDTO>).ToList();
		}


		/// <summary>
		/// Метод возвращает список сущностей ролей по полученному списку наименований ролей у пользователя.
		/// </summary>
		public async Task<List<RoleDTO>> GetRolesByNamesList(IEnumerable<string> roleNamesList)
		{
			var roleEntityList = await dbContext.Roles.Where(r => roleNamesList.Contains(r.Name)).ToListAsync();
			return roleEntityList.Select(mapper.Map<RoleDTO>).ToList();
		}


		/// <summary>
		/// Метод возвращает список клаймов для пользователя.
		/// </summary>
		public List<Claim> GetClaimsByUser(UserDTO userDTO)
		{
			
			var roleEntityList = userDTO.RolesList.Select(mapper.Map<RoleEntity>).ToList();
			var roleClaims = roleEntityList.SelectMany(role => role.RoleClaims).Distinct().ToList();
			var userClaims = new List<Claim> { new(ClaimTypes.Name, userDTO.UserName) };
			roleClaims.ForEach(roleClaim => userClaims.Add(new Claim(ClaimTypes.Role, roleClaim.ToString())));
			return userClaims;
		}
	}
}
