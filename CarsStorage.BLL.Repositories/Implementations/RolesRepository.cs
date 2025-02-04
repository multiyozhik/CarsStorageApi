using CarsStorage.BLL.Repositories.Interfaces;
using CarsStorage.DAL.EF;
using CarsStorage.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CarsStorage.BLL.Repositories.Implementations
{
	/// <summary>
	/// Класс репозитория для ролей пользователя.
	/// </summary>
	public class RolesRepository(UsersRolesDbContext dbContext) : IRolesRepository
	{
		/// <summary>
		/// Метод получения всего списка возможных ролей.
		/// </summary>
		public async Task<List<RoleEntity>> GetList()
		{
			return await dbContext.Roles.ToListAsync();
		}


		/// <summary>
		/// Метод возвращает сущность роли по id пользователя.
		/// </summary>
		public async Task<RoleEntity> GetRoleById(int id)
		{
			var role = await dbContext.Roles.FirstOrDefaultAsync(r => r.Id == id);
			return role is not null
				? role 
				: throw new Exception("Роль с заданным Id не найдена");
		}
		//ToDo: посмотреть, тут надо изменить GetRoleById, т.к. должен возвращать список ролей.


		/// <summary>
		/// Метод возвращает список сущностей ролей по полученному списку наименований ролей у пользователя.
		/// </summary>
		public async Task<List<RoleEntity>> GetRolesByNamesList(IEnumerable<string> roleNamesList)
		{
			var rolesList = await GetList();
			return roleNamesList.Select(roleName => rolesList.FirstOrDefault(r => r.Name == roleName)).ToList();	
		}


		/// <summary>
		/// Метод возвращает список клаймов по полученной сущности пользователя.
		/// </summary>
		public List<Claim> GetClaimsByUser(UserEntity userEntity)
		{
			var roleEntityList = userEntity.RolesList;
			var roleClaims = roleEntityList.SelectMany(role => role.RoleClaims).Distinct().ToList();
			var userClaims = new List<Claim> { new(ClaimTypes.Name, userEntity.UserName) };
			roleClaims.ForEach(roleClaim => userClaims.Add(new Claim(ClaimTypes.Role, roleClaim.ToString())));
			return userClaims;
		}
	}
}
