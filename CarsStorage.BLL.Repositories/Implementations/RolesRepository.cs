﻿using CarsStorage.BLL.Repositories.Interfaces;
using CarsStorage.DAL.EF;
using CarsStorage.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CarsStorage.BLL.Repositories.Implementations
{
	public class RolesRepository(IdentityAppDbContext dbContext) : IRolesRepository
	{
		public async Task<List<RoleEntity>> GetList()
		{
			return await dbContext.Roles.ToListAsync();
		}

		public async Task<RoleEntity> GetRoleById(int id)
		{
			var role = await dbContext.Roles.FirstOrDefaultAsync(r => r.Id == id);
			return role is not null ? role : throw new Exception("Роль с заданным Id не найдена");
		}

		public async Task<List<RoleEntity>> GetRolesByNamesList(IEnumerable<string> roleNamesList)
		{
			var rolesList = (await GetList()).ToList();
			return roleNamesList.Select(roleName => rolesList.FirstOrDefault(r => r.Name == roleName)).ToList();	
		}

		public List<Claim> GetClaimsByUser(IdentityAppUser identityAppUser)
		{
			var roleEntityList = identityAppUser.RolesList;
			var roleClaims = roleEntityList.SelectMany(role => role.RoleClaims).Distinct().ToList();
			var userClaims = new List<Claim> { new(ClaimTypes.Name, identityAppUser.UserName) };
			roleClaims.ForEach(roleClaim => userClaims.Add(new Claim(ClaimTypes.Role, roleClaim.ToString())));
			return userClaims;
		}
	}
}
