using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarsStorage.BLL.Abstractions
{
	public interface IAccountService
	{
		public Task<StatusCodeResult> LogIn(AppUser appUser);
		public Task LogOut();
	}
}
