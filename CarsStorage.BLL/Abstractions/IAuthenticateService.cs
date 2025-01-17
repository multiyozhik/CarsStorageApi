﻿using CarsStorage.BLL.Config;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarsStorage.BLL.Abstractions
{
	public interface IAuthenticateService
	{
		public Task<IActionResult> Register(string userName, string email, string password);
		public Task<ActionResult<string>> LogIn(string userName, string password);
		public Task LogOut();
	}
}
