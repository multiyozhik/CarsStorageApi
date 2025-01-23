using CarsStorage.BLL.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsStorage.BLL.Abstractions.Interfaces
{
	public interface IRolesService
	{
		public Task<IEnumerable<RoleDTO>> GetList();
		public Task Create(RoleCreaterDTO roleCreaterDTO);
		public Task Update(RoleDTO roleDTO);
		public Task Delete(int id);
	}
}
