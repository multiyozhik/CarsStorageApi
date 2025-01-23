using CarsStorage.BLL.Abstractions.Interfaces;
using CarsStorage.BLL.Abstractions.Models;
using CarsStorage.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CarsStorage.BLL.Implementations.Services
{
	public class AuthenticateService(
		SignInManager<IdentityAppUser> signInManager, 
		UserManager<IdentityAppUser> userManager,
		IOptions<JWTConfigDTO> jwtConfigDTO) : IAuthenticateService
	{
		private readonly JWTConfigDTO jwtConfig = jwtConfigDTO.Value;
		public async Task<IActionResult> Register(AppUserRegisterDTO registerAppUser)
		{
			var user = new IdentityAppUser { UserName = registerAppUser.UserName, Email = registerAppUser.Email };
			var result = await userManager.CreateAsync(user, registerAppUser.Password);
			if (result.Succeeded) {				
				foreach (var role in registerAppUser.Roles)
					await userManager.AddToRoleAsync(user, role);
				await signInManager.SignInAsync(user, false);
			}
			return new BadRequestObjectResult("Ошибка ввода данных пользователя");
		}

		public async Task<ActionResult<JWTTokenDTO>> LogIn(AppUserLoginDTO appUserLoginDTO) 
		{
			var user = await userManager.FindByNameAsync(appUserLoginDTO.UserName);
			if (user is null)
				return new BadRequestObjectResult("Пользователь не найден");

			if (string.IsNullOrEmpty(jwtConfig.Key))
				throw new Exception("Не задан секретный ключ для конфигурации jwt авторизации");

			await signInManager.SignOutAsync();
			var result = await signInManager.PasswordSignInAsync(user, appUserLoginDTO.Password, false, false);
			if (!result.Succeeded)
				return new StatusCodeResult(401);

			if (jwtConfig.Key is null)
				throw new Exception("Не задан секретный ключ при конфигурации аутентификации");
			var roles = await userManager.GetRolesAsync(user);
			if (roles is not null && user.UserName is not null)
			{
				//rolesClaims = roles.RolesClaims  список признаков для роли
				//var claimsList = new List<Claim> { new(ClaimTypes.Name, user.UserName) }; сразу передать claimsList из при создании роли

				var claimsList = new List<Claim> { new(ClaimTypes.Name, user.UserName) };
				foreach (var role in roles)
					claimsList.Add(new Claim(ClaimTypes.Role, role));
				var jwt = new JwtSecurityToken(
					claims: claimsList,    //сюда передать список
					issuer: jwtConfig.Issuer,
					audience: jwtConfig.Audience,
					expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(jwtConfig.ExpireMinutes)),
					signingCredentials: new SigningCredentials(new SymmetricSecurityKey(
						Convert.FromBase64String(jwtConfig.Key)), SecurityAlgorithms.HmacSha256));




					return new JWTTokenDTO { Token = new JwtSecurityTokenHandler().WriteToken(jwt) };
			}
			return new StatusCodeResult(401);	
		}

		public async Task LogOut() 
		{	
			await signInManager.SignOutAsync();
		}
	}
}
