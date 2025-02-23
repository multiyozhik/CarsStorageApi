using AutoMapper;
using CarsStorage.Abstractions.BLL.Services;
using CarsStorage.Abstractions.DAL.Repositories;
using CarsStorage.Abstractions.Exceptions;
using CarsStorage.Abstractions.General;
using CarsStorage.Abstractions.ModelsDTO.User;
using CarsStorage.BLL.Abstractions.ModelsDTO.User;
using CarsStorage.BLL.Services.Utils;
using CarsStorage.DAL.Entities;

namespace CarsStorage.BLL.Services.Services
{
	/// <summary>
	/// Класс сервиса пользователей.
	/// </summary>
	public class UsersService(IUsersRepository usersRepository, IMapper mapper, IPasswordHasher passwordHasher) : IUsersService
	{
		/// <summary>
		/// Метод возвращает результат с списком всех пользователей.
		/// </summary>
		public async Task<ServiceResult<List<UserDTO>>> GetList()
		{
			try
			{
				var userEntityList = await usersRepository.GetList();
				var userDTOList = userEntityList.Select(mapper.Map<UserDTO>).ToList();
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
				var userEntity = await usersRepository.GetUserByUserId(id);
				return new ServiceResult<UserDTO>(mapper.Map<UserDTO>(userEntity));
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
				var userEntity = mapper.Map<UserEntity>(userCreaterDTO);
				var hashedPassword = passwordHasher.HashPassword(userCreaterDTO.Password);
				userEntity.Hash = hashedPassword.Hash;
				userEntity.Salt = hashedPassword.Salt;
				if (userCreaterDTO.RoleNamesList is null)
					throw new Exception("Не определены роли пользователя.");
				userEntity.RolesList = await usersRepository.GetRolesByRoleNames(userCreaterDTO.RoleNamesList);
				var createdUserEntity = await usersRepository.Create(userEntity);
				return new ServiceResult<UserDTO>(mapper.Map<UserDTO>(createdUserEntity));
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
				var userEntity = mapper.Map<UserEntity>(userUpdaterDTO);
				if (userUpdaterDTO.RoleNamesList is null)
					throw new Exception("Не определены роли пользователя.");
				userEntity.RolesList = await usersRepository.GetRolesByRoleNames(userUpdaterDTO.RoleNamesList);
				var updatedUserEntity = await usersRepository.Update(userEntity);
				return new ServiceResult<UserDTO>(mapper.Map<UserDTO>(updatedUserEntity));
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
