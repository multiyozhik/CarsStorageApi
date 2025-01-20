using CarsStorage.BLL.AuthModels;
using CarsStorage.BLL.Config;
using Microsoft.AspNetCore.Mvc;

namespace CarsStorage.BLL.Abstractions
{
	public interface IAuthenticateService
	{
		public Task<IActionResult> Register(RegisterAppUser registerAppUser);
		public Task<ActionResult<TokenJWT>> LogIn(string userName, string password, JWTConfig jWTConfig);
		public Task LogOut();
	}
}
