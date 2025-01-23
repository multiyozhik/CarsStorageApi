using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsStorage.BLL.Repositories.Interfaces
{
	public interface IUsersRepository
	{
		public Task<IEnumerable<AppUserDTO>> GetList();
		public Task<ActionResult<AppUserDTO>> GetById(int id);
		public Task<IActionResult> Create(AppUserCreaterDTO appUserCreaterDTO);
		public Task<IActionResult> Update(AppUserDTO appUser);
		public Task<IActionResult> Delete(int id);
	}
}
