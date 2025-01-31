using CarsStorage.BLL.Abstractions.Models;
using CarsStorage.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsStorage.BLL.Abstractions.Interfaces
{
	public interface IRolesService
	{
		public Task<ServiceResult<IEnumerable<RoleDTO>>> GetList();
		public Task<ServiceResult<RoleDTO>> GetRoleById(int id);
		public Task<ServiceResult<IEnumerable<RoleEntity>>> GetRolesByNamesList(IEnumerable<string> roleNamesList);
	}
}
