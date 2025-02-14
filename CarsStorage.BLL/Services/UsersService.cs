using CarsStorage.Abstractions.BLL.Services;
using CarsStorage.Abstractions.DAL.Repositories;
using CarsStorage.Abstractions.Exceptions;
using CarsStorage.Abstractions.General;
using CarsStorage.Abstractions.ModelsDTO.Token;
using CarsStorage.Abstractions.ModelsDTO.User;
using CarsStorage.BLL.Abstractions.ModelsDTO.User;
using System;

namespace CarsStorage.BLL.Services.Services
{
	/// <summary>
	/// Класс сервиса пользователей.
	/// </summary>
	public class UsersService(IUsersRepository usersRepository) : IUsersService
	{
		/// <summary>
		/// Метод возвращает результат с списком всех пользователей.
		/// </summary>
		public async Task<ServiceResult<List<UserDTO>>> GetList()
		{
			try
			{
				var userDTOList = await usersRepository.GetList();
				return new ServiceResult<List<UserDTO>>(userDTOList);
			}
			catch (Exception exception)
			{
				return new ServiceResult<List<UserDTO>>(new NotFoundException(exception.Message));
			}
		}


		/// <summary>
		/// Метод возвращает результат с пользователем по полученному id.
		/// </summary>
		public async Task<ServiceResult<UserDTO>> GetById(int id)
		{
			try
			{
				var userDTO = await usersRepository.GetById(id);
				return new ServiceResult<UserDTO>(userDTO);
			}
			catch (Exception exception)
			{
				return new ServiceResult<UserDTO>(new NotFoundException(exception.Message));
			}
		}


		/// <summary>
		/// Метод создает пользователя (с паролем и ролями) и возвращает как результат созданного пользователя.
		/// </summary>
		public async Task<ServiceResult<UserDTO>> Create(UserCreaterDTO userCreaterDTO)
		{
			try
			{
				var userDTO = await usersRepository.Create(userCreaterDTO);
				return new ServiceResult<UserDTO>(userDTO);
			}
			catch (Exception exception)
			{
				return new ServiceResult<UserDTO>(new BadRequestException(exception.Message));
			}
		}


		/// <summary>
		/// Метод изменяет пользователя и возвращает как результат его.
		/// </summary>
		public async Task<ServiceResult<UserDTO>> Update(UserUpdaterDTO userUpdaterDTO)
		{
			try
			{
				var user = await usersRepository.Update(userUpdaterDTO);
				return new ServiceResult<UserDTO>(user);
			}
			catch (Exception exception)
			{
				return new ServiceResult<UserDTO>(new BadRequestException(exception.Message));
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
				return new ServiceResult<int>(id);
			}
			catch (Exception exception)
			{
				return new ServiceResult<int>(new BadRequestException(exception.Message));
			}
		}
	}
}
