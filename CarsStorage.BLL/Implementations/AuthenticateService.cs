using CarsStorage.BLL.Abstractions;
using CarsStorage.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;



namespace CarsStorage.BLL.Implementations
{
	public class AuthenticateService(SignInManager<IdentityAppUser> signInManager, UserManager<IdentityAppUser> userManager, IConfiguration config) : IAuthenticateService
	{
		private readonly SignInManager<IdentityAppUser> signInManager = signInManager;
		private readonly UserManager<IdentityAppUser> userManager = userManager;
		private readonly IConfiguration config = config;

		public async Task<IActionResult> Register(string userName, string email, string password)
		{
			var user = new IdentityAppUser { UserName = userName, Email = email };
			var result = await userManager.CreateAsync(user, password);
			if (result.Succeeded) {
				var res = await userManager.AddToRoleAsync(user, config["RoleNamesConfig:AuthUserRoleName"]);
				if (res.Succeeded)
				{
					await signInManager.SignInAsync(user, false);
					return new StatusCodeResult(200);
				}
				else
				{
					return new StatusCodeResult(500);
				}
			}
			else
			{
				return new StatusCodeResult(500);
			}
		}

		public async Task<ActionResult<string>> LogIn(string userName, string password)
		{
			var user = await userManager.FindByNameAsync(userName);
			if (user is not null)
			{
				await signInManager.SignOutAsync();
				var result = await signInManager.PasswordSignInAsync(
					user, password, false, false);
				var roles = await userManager.GetRolesAsync(user);
				var claimsList = new List<Claim> { new(ClaimTypes.Name, user.UserName) };				
				if (roles != null)
				{
					foreach (var role in roles)
					{
						claimsList.Add(new Claim(ClaimTypes.Role, role));
					}
				}

				if (result.Succeeded)
				{
					var jwt = new JwtSecurityToken(
						claims: claimsList,
						issuer: config["JWT:Issuer"],
						audience: config["JWT:Audience"],
						expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(60)),
						signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"])), SecurityAlgorithms.HmacSha256));

					return new JwtSecurityTokenHandler().WriteToken(jwt);
					//return new TokenJWT { Token = new JwtSecurityTokenHandler().WriteToken(jwt) };
				}
				return new StatusCodeResult(400);   //Bad Request, например, пароль неверный
			}
			return new StatusCodeResult(401);		//не авторизован, т.к. не найден в БД	
		}

		public async Task LogOut()      //возможно httpcontext передавать?
		{	
			await signInManager.SignOutAsync();
		}
	}
}
