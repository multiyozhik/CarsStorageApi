using AutoMapper;
using CarsStorage.BLL.Abstractions.Interfaces;
using CarsStorage.BLL.Abstractions.Models;
using CarsStorage.BLL.Repositories.Interfaces;
using CarsStorage.DAL.Entities;
using CarsStorage.DAL.Models;
using System.Security.Claims;

namespace CarsStorage.BLL.Implementations.Services
{
	public class UsersService(IUsersRepository usersRepository, IMapper mapper) : IUsersService
	{
		private List<Claim> claims = [];
		public async Task<ServiceResult<List<AppUserDTO>>> GetList()
		{
			try
			{
				var identityAppUsersList = await usersRepository.GetList();
				var appUserDTOList = identityAppUsersList.Select(mapper.Map<AppUserDTO>).ToList();
				return new ServiceResult<List<AppUserDTO>>(appUserDTOList, null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<List<AppUserDTO>>(null, exception.Message);
			}
		}


		public async Task<ServiceResult<AppUserDTO>> GetById(int id)
		{
			try
			{
				var identityAppUser = await usersRepository.GetById(id);
				return new ServiceResult<AppUserDTO>(mapper.Map<AppUserDTO>(identityAppUser), null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<AppUserDTO>(null, exception.Message);
			}
		}


		public async Task<ServiceResult<AppUserDTO>> Create(AppUserCreaterDTO appUserCreaterDTO)
		{
			try
			{
				var identityAppUser = await usersRepository.Create(mapper.Map<IdentityAppUserCreater>(appUserCreaterDTO));
				return new ServiceResult<AppUserDTO>(mapper.Map<AppUserDTO>(identityAppUser), null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<AppUserDTO>(null, exception.Message);
			}
		}


		public async Task<ServiceResult<AppUserDTO>> Update(AppUserDTO appUserDTO)
		{
			try
			{
				var identityAppUser = await usersRepository.Update(mapper.Map<IdentityAppUser>(appUserDTO));
				return new ServiceResult<AppUserDTO>(mapper.Map<AppUserDTO>(identityAppUser), null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<AppUserDTO>(null, exception.Message);
			}
		}

		public async Task<ServiceResult<int>> Delete(int id)
		{
			try
			{
				await usersRepository.Delete(id);
				return new ServiceResult<int>(id, null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<int>(id, exception.Message);
			}
		}

		public async Task<ServiceResult<AppUserDTO>> GetUserByRefreshToken(string refreshToken)
		{
			try
			{
				var user = await usersRepository.GetUserByRefreshToken(refreshToken);
				return new ServiceResult<AppUserDTO>(user, null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<AppUserDTO>(null, exception.Message);
			}			
		}

		//public async Task<Claim> GetClaims(AppUserDTO appUserDTO)
		//{
		//	//составляем общий список клаймов userClaims, имеющийся от всех его ролей (неповторяющийся)
		//	var roleClaims = appUserDTO.Roles.SelectMany(role => role.RoleClaims).Distinct().ToList();
		//	var userClaims = new List<Claim> { new(ClaimTypes.Name, user.UserName) };
		//	roleClaims.ForEach(roleClaim => userClaims.Add(new Claim(ClaimTypes.Role, roleClaim.ToString())));
		//}



		//public async Task<ServiceResult<AppUserDTO>> AddToRole(RoleDTO roleDTO)
		//{
		//	try
		//	{
		//		await usersRepository.AddToRole(RoleDTO roleDTO);
		//		return new ServiceResult(id, null);
		//	}
		//	catch (Exception exception)
		//	{
		//		return new ServiceResult<int>(id, exception.Message);
		//	}
		//}
	}
}
