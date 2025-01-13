using CarsStorage.BLL.Abstractions;
using CarsStorage.BLL.Servises;
using CarsStorage.DAL.EF;
using CarsStorage.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsStorage.BLL.Implementations
{
	public class AdminUsersService : IAdminUsersService
	{
		public Task AddAsync(AppUser appUser)
		{
			throw new NotImplementedException();
		}

		public Task DeleteAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<AppUser>> GetUsersList()
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync(AppUser appUser)
		{
			throw new NotImplementedException();
		}
	}
}
