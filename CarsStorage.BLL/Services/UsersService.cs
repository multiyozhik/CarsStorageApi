using AutoMapper;
using CarsStorage.BLL.Abstractions.Interfaces;
using CarsStorage.BLL.Abstractions.Models;
using CarsStorage.BLL.Abstractions.ModelsDTO.UserDTO;
using CarsStorage.BLL.Repositories.Interfaces;
using CarsStorage.DAL.Entities;
using CarsStorage.DAL.Models;
using System.Security.Claims;

namespace CarsStorage.BLL.Implementations.Services
{
    public class UsersService(IUsersRepository usersRepository, IRolesRepository rolesRepository,  IMapper mapper) : IUsersService
	{
		public async Task<ServiceResult<List<UserDTO>>> GetList()
		{
			try
			{
				var identityAppUsersList = await usersRepository.GetList();
				var appUserDTOList = identityAppUsersList.Select(mapper.Map<UserDTO>).ToList();
				return new ServiceResult<List<UserDTO>>(appUserDTOList, null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<List<UserDTO>>(null, exception.Message);
			}
		}


		public async Task<ServiceResult<UserDTO>> GetById(int id)
		{
			try
			{
				var identityAppUser = await usersRepository.GetById(id);
				return new ServiceResult<UserDTO>(mapper.Map<UserDTO>(identityAppUser), null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<UserDTO>(null, exception.Message);
			}
		}


		public async Task<ServiceResult<UserDTO>> Create(UserCreaterDTO userCreaterDTO)
		{
			try
			{
				var userEntity = await usersRepository.Create(mapper.Map<UserCreater>(userCreaterDTO));
				return new ServiceResult<UserDTO>(mapper.Map<UserDTO>(userEntity), null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<UserDTO>(null, exception.Message);
			}
		}


		public async Task<ServiceResult<UserDTO>> Update(UserDTO userDTO)
		{
			try
			{
				var userEntity = await usersRepository.Update(mapper.Map<UserEntity>(userDTO));
				return new ServiceResult<UserDTO>(mapper.Map<UserDTO>(userEntity), null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<UserDTO>(null, exception.Message);
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

		public async Task<ServiceResult<UserDTO>> GetUserByRefreshToken(string refreshToken)
		{
			try
			{
				var user = await usersRepository.GetUserByRefreshToken(refreshToken);
				return new ServiceResult<UserDTO>(user, null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<UserDTO>(null, exception.Message);
			}			
		}
	}
}
