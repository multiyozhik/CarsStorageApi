using CarsStorage.BLL.Servises;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsStorage.BLL.Abstractions
{
	public interface IAdminUsersService
	{
		public Task<IEnumerable<AppUser>> GetUsersList();
		public Task AddAsync(AppUser appUser);
		public Task UpdateAsync(AppUser appUser);
		public Task DeleteAsync(Guid id);
	}
}
