using CarsStorage.BLL.Config;
using Microsoft.AspNetCore.Mvc;

namespace CarsStorage.BLL.Abstractions
{
	public interface IUsersService
	{
		public Task<IEnumerable<AppUser>> GetList();
		public Task<ActionResult<AppUser>> GetById(Guid id);
		public Task<IActionResult> Create(RegisterAppUser registerAppUser, RoleNames roleNames);
		public Task<IActionResult> Update(AppUser appUser);
		public Task<IActionResult> Delete(Guid id);
	}
}
