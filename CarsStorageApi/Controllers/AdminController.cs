using CarsStorage.BLL.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarsStorageApi.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class AdminController : Controller
	{
		private readonly IAdminService adminUsersService;
		public AdminController(IAdminService adminUsersService) 
		{
			this.adminUsersService = adminUsersService;
		}

		//[HttpPost]
		//public IEnumerable< GetUsers





	}
}

