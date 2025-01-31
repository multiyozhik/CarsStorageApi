using AutoMapper;
using CarsStorage.BLL.Abstractions.Interfaces;
using CarsStorage.BLL.Abstractions.Models;
using CarsStorage.DAL.Entities;
using CarsStorage.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CarsStorage.BLL.Implementations.Services
{
	public class AuthenticateService(
		SignInManager<IdentityAppUser> signInManager,
		UserManager<IdentityAppUser> userManager, ITokenService tokenService, IRolesService rolesService, IUsersService usersService,
		IOptions<JWTConfigDTO> jwtConfigDTO, IMapper mapper, IOptions<InitialDbSeedConfig> initialOptions) : IAuthenticateService
	{
		private readonly JWTConfigDTO jwtConfig = jwtConfigDTO.Value;

		public async Task<ServiceResult<AppUserDTO>> Register(AppUserRegisterDTO appUserRegisterDTO)
		{
			try
			{
				var user = new IdentityAppUser { UserName = appUserRegisterDTO.UserName, Email = appUserRegisterDTO.Email };
				var result = await userManager.CreateAsync(user, appUserRegisterDTO.Password);
				var defaultRoles = new List<string>() { initialOptions.Value.DefaultRoleName };

				//var rolesServiceResult = await rolesService.GetList();
				//if (rolesServiceResult.IsSuccess)
				//	var roles = rolesServiceResult.Result;


				//var userRoles = new List<RoleDTO>();
				//defaultRoles.Select(r => userRoles.Add());

				//defaultRoles.Select(roleName => rolesService.GetRoleByName(roleName));
				//if (result.Succeeded)
				//{
				//	user.RolesList = defaultRoles;
				//	await signInManager.SignInAsync(user, false);
				//}
				return new ServiceResult<AppUserDTO>(mapper.Map<AppUserDTO>(user), null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<AppUserDTO>(null, exception.Message);
			}
		}

		public async Task<ServiceResult<JWTTokenDTO>> LogIn(AppUserLoginDTO appUserLoginDTO) 
		{
			try
			{
				var user = await userManager.FindByNameAsync(appUserLoginDTO.UserName);
				if (user is null) throw new Exception("Ошибка - неверный логин.");

				await signInManager.SignOutAsync();
				var result = await signInManager.PasswordSignInAsync(user, appUserLoginDTO.Password, false, false);
				if (!result.Succeeded) throw new Exception("Ошибка - неверный пароль.");

				var roleClaims = user.RolesList.SelectMany(role => role.RoleClaims).Distinct();
				var userClaims = new List<Claim> { new(ClaimTypes.Name, user.UserName) };					
				foreach (var claim in roleClaims)
				{
					userClaims.Add(new Claim(ClaimTypes.Role, claim.ToString()));
				}
				var jwt = new JwtSecurityToken(
					claims: userClaims,
					issuer: jwtConfig.Issuer,
					audience: jwtConfig.Audience,
					expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(jwtConfig.ExpireMinutes)),
					signingCredentials: new SigningCredentials(new SymmetricSecurityKey(
						Convert.FromBase64String(jwtConfig.Key)), SecurityAlgorithms.HmacSha256));
				var jwtTokenDTO = new JWTTokenDTO()
				{
					Token = new JwtSecurityTokenHandler().WriteToken(jwt)
				};
				return new ServiceResult<JWTTokenDTO>(jwtTokenDTO, null);

				//var accessToken = tokenService.GetAccessToken();
				//string tokenString = tokenService.GetAccessToken(
				//	user.GetUserClaims(), out DateTime expires);
				//SetUserAuthData(user, tokenString, expires, tokenService, context);
				//return Results.Ok(
				//	new
				//	{
				//		token = tokenString,
				//		refreshToken = user.RefreshToken,
				//		tokenExpires = expires,
				//		user = login
				//	});


			}
			catch (Exception exception)
			{
				return new ServiceResult<JWTTokenDTO>(null, exception.Message);
			}
		}

		public async Task LogOut() 
		{	
			await signInManager.SignOutAsync();
		}
	}
}
