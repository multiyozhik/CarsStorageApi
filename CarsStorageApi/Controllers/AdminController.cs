using CarsStorage.BLL.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarsStorageApi.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class AdminController : Controller
	{
		private readonly IAdminUsersService adminUsersService;
		public AdminController(IAdminUsersService adminUsersService) 
		{
			this.adminUsersService = adminUsersService;
		}





	}
}

