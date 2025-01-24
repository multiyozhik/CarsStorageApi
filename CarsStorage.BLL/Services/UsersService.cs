using AutoMapper;
using CarsStorage.BLL.Abstractions.Interfaces;
using CarsStorage.BLL.Abstractions.Models;
using CarsStorage.BLL.Repositories.Interfaces;
using CarsStorage.DAL.Entities;
using CarsStorage.DAL.Models;

namespace CarsStorage.BLL.Implementations.Services
{
	public class UsersService(IUsersRepository usersRepository, IMapper mapper) : IUsersService
	{
		public async Task<ServiceResult<IEnumerable<AppUserDTO>>> GetList()
		{
			try
			{
				var identityAppUsersList = await usersRepository.GetList();
				var appUserDTOList = identityAppUsersList.Select(mapper.Map<AppUserDTO>);
				return new ServiceResult<IEnumerable<AppUserDTO>>(appUserDTOList, null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<IEnumerable<AppUserDTO>>(null, exception.Message);
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
	}
}
