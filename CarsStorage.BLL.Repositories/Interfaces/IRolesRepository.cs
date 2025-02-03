using CarsStorage.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CarsStorage.BLL.Repositories.Interfaces
{
	public interface IRolesRepository
	{
		public Task<List<RoleEntity>> GetList();
		public Task<RoleEntity> GetRoleById(int id);
		public Task<List<RoleEntity>> GetRolesByNamesList(IEnumerable<string> roleNamesList);
		public List<Claim> GetClaimsByUser(AppUserEntity identityAppUser);
	}
}
