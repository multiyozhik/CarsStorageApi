using CarsStorage.BLL.AuthModels;
using CarsStorage.BLL.Config;
using Microsoft.AspNetCore.Mvc;

namespace CarsStorage.BLL.Abstractions
{
	public interface IAuthenticateService
	{
		public Task<IActionResult> Register(string userName, string email, string password);
		public Task<ActionResult<TokenJWT>> LogIn(string userName, string password, JWTConfig jWTConfig);
		public Task LogOut();
	}
}
