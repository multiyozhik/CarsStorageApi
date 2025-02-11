using AutoMapper;
using CarsStorage.BLL.Abstractions.ModelsDTO.Role;
using CarsStorage.BLL.Abstractions.Repositories;
using CarsStorage.DAL.DbContexts;
using Microsoft.EntityFrameworkCore;

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
	}
}
