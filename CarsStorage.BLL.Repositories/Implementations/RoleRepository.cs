using CarsStorage.BLL.Repositories.Interfaces;
using CarsStorage.DAL.EF;
using CarsStorage.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarsStorage.BLL.Repositories.Implementations
{
	public class RoleRepository(IdentityAppDbContext dbContext) : IRoleRepository
	{
		public async Task<IEnumerable<RoleEntity>> GetList()
		{
			return await dbContext.Roles.ToListAsync();
		}

		public async Task<RoleEntity> GetRoleById(int id)
		{
			var role = await dbContext.Roles.FirstOrDefaultAsync(r => r.Id == id);
			return role is not null ? role : throw new Exception("Роль с заданным Id не найдена");
		}

		public async Task<IEnumerable<RoleEntity>> GetRolesByNamesList(IEnumerable<string> roleNamesList)
		{
			var rolesList = (await GetList()).ToList();
			return roleNamesList.Select(roleName => rolesList.FirstOrDefault(r => r.Name == roleName));	
		}
	}
}
