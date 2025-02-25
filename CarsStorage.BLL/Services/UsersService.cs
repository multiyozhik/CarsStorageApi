using AutoMapper;
using CarsStorage.Abstractions.BLL.Services;
using CarsStorage.Abstractions.DAL.Repositories;
using CarsStorage.Abstractions.Exceptions;
using CarsStorage.Abstractions.General;
using CarsStorage.Abstractions.ModelsDTO.User;
using CarsStorage.BLL.Abstractions.ModelsDTO.User;
using CarsStorage.BLL.Services.Utils;
using CarsStorage.DAL.Entities;
using Microsoft.Extensions.Logging;

namespace CarsStorage.BLL.Services.Services
{
	/// <summary>
	/// Класс сервиса пользователей.
	/// </summary>
	/// <param name="usersRepository">Репозиторий пользователей.</param>
	/// <param name="mapper">Объект мепппера.</param>
	/// <param name="passwordHasher">Объект для хеширования паролей.</param>
	/// <param name="logger">Объект для выполнения логирования.</param>
	public class UsersService(IUsersRepository usersRepository, IMapper mapper, IPasswordHasher passwordHasher, ILogger<UsersService> logger) : IUsersService
	{
		/// <summary>
		/// Метод для получения списка всех пользователей.
		/// </summary>
		/// <returns>Список всех пользователей.</returns>
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
				logger.LogError("Ошибка в {service} в {method} при получении списка всех пользователей: {errorMessage}", this, nameof(this.GetList), exception.Message);
				return new ServiceResult<List<UserDTO>>(new NotFoundException(exception.Message));
			}
		}


		/// <summary>
		/// Метод для получения объекта пользователя по его идентификатору.
		/// </summary>
		/// <param name="id">Идентификатор пользователя.</param>
		/// <returns>Объект пользователя.</returns>
		public async Task<ServiceResult<UserDTO>> GetById(int id)
		{
			try
			{
				var userEntity = await usersRepository.GetUserByUserId(id);
				return new ServiceResult<UserDTO>(mapper.Map<UserDTO>(userEntity));
			}
			catch (Exception exception)
			{
				logger.LogError("Ошибка в {service} в {method} при получении объекта пользователя по его идентификатору: {errorMessage}", this, nameof(this.GetById), exception.Message);
				return new ServiceResult<UserDTO>(new NotFoundException(exception.Message));
			}
		}


		/// <summary>
		/// Метод для создания объекта пользователя.
		/// </summary>
		/// <param name="userCreaterDTO">Объект пользователя для создания.</param>
		/// <returns>Созданный объект пользователя.</returns>
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
				logger.LogError("Ошибка в {service} в {method} при создании объекта пользователя: {errorMessage}", this, nameof(this.Create), exception.Message);
				return new ServiceResult<UserDTO>(new BadRequestException(exception.Message));
			}
		}


		/// <summary>
		/// Метод для изменения объекта пользователя.
		/// </summary>
		/// <param name="userUpdaterDTO">Объект пользователя для изменения.</param>
		/// <returns>Измененный объект пользователя.</returns>
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
				logger.LogError("Ошибка в {service} в {method} при изменении объекта пользователя: {errorMessage}", this, nameof(this.Update), exception.Message);
				return new ServiceResult<UserDTO>(new BadRequestException(exception.Message));
			}
		}

		/// <summary>
		/// Метод для удаления объекта пользователя по его идентификатору.
		/// </summary>
		/// <param name="id">Идентификатор пользователя.</param>
		/// <returns>Идентификатор удаленного пользователя.</returns>
		public async Task<ServiceResult<int>> Delete(int id)
		{
			try
			{
				await usersRepository.Delete(id);
				return new ServiceResult<int>(id);
			}
			catch (Exception exception)
			{
				logger.LogError("Ошибка в {service} в {method} при удалении объекта пользователя по его идентификатору: {errorMessage}", this, nameof(this.Delete), exception.Message);
				return new ServiceResult<int>(new BadRequestException(exception.Message));
			}
		}
	}
}
