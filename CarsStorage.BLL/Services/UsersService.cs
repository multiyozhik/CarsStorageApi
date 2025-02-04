using AutoMapper;
using CarsStorage.BLL.Abstractions.Interfaces;
using CarsStorage.BLL.Abstractions.Models;
using CarsStorage.BLL.Abstractions.ModelsDTO.UserDTO;
using CarsStorage.BLL.Repositories.Interfaces;
using CarsStorage.DAL.Entities;
using CarsStorage.DAL.Models;

namespace CarsStorage.BLL.Implementations.Services
{
	/// <summary>
	/// Класс сервиса пользователей.
	/// </summary>
	public class UsersService(IUsersRepository usersRepository, IRolesRepository rolesRepository,  IMapper mapper) : IUsersService
	{
		/// <summary>
		/// Метод возвращает результат с списком всех пользователей.
		/// </summary>
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


		/// <summary>
		/// Метод возвращает результат с пользователем по полученному id.
		/// </summary>
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


		/// <summary>
		/// Метод создает пользователя (с паролем и ролями) и возвращает как результат созданного пользователя.
		/// </summary>
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


		/// <summary>
		/// Метод изменяет пользователя и возвращает как результат его.
		/// </summary>
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

		/// <summary>
		/// Метод удаляет пользователя по id и возвращает id этого удаленного пользователя.
		/// </summary>
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

		/// <summary>
		/// Метод получения как результат пользователя по значению refresh-токена.
		/// </summary>
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
