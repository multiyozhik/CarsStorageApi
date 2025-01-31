using CarsStorage.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsStorage.BLL.Repositories.Interfaces
{
	public interface IRoleRepository
	{
		public Task<IEnumerable<RoleEntity>> GetList();
		public Task<RoleEntity> GetRoleById(int id);
		public Task<IEnumerable<RoleEntity>> GetRolesByNamesList(IEnumerable<string> roleNamesList);
	}
}
