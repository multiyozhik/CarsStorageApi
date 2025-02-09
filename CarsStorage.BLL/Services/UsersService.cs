using AutoMapper;
using CarsStorage.BLL.Abstractions.Exceptions;
using CarsStorage.BLL.Abstractions.General;
using CarsStorage.BLL.Abstractions.Services;
using CarsStorage.BLL.Abstractions.ModelsDTO.User;
using CarsStorage.BLL.Abstractions.Repositories;

namespace CarsStorage.BLL.Implementations.Services
{
	/// <summary>
	/// Класс сервиса пользователей.
	/// </summary>
	public class UsersService(IUsersRepository usersRepository, IRolesService rolesService, IMapper mapper) : IUsersService
	{
		/// <summary>
		/// Метод возвращает результат с списком всех пользователей.
		/// </summary>
		public async Task<ServiceResult<List<UserDTO>>> GetList()
		{
			try
			{
				var userDTOList = await usersRepository.GetList();
				return new ServiceResult<List<UserDTO>>(userDTOList, null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<List<UserDTO>>(null, new NotFoundException(exception.Message));
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
				return new ServiceResult<UserDTO>(userDTO, null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<UserDTO>(null, new NotFoundException(exception.Message));
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
				return new ServiceResult<UserDTO>(userDTO, null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<UserDTO>(null, new BadRequestException(exception.Message));
			}
		}


		/// <summary>
		/// Метод изменяет пользователя и возвращает как результат его.
		/// </summary>
		public async Task<ServiceResult<UserDTO>> Update(UserDTO userDTO)
		{
			try
			{
				var user = await usersRepository.Update(userDTO);
				return new ServiceResult<UserDTO>(user, null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<UserDTO>(null, new BadRequestException(exception.Message));
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
				return new ServiceResult<int>(id, new BadRequestException(exception.Message));
			}
		}

		///// <summary>
		///// Метод возвращает id пользователя по значению refresh-токена.
		///// </summary>
		//public async Task<ServiceResult<int>> GetUserByRefreshToken(string refreshToken)
		//{
		//	try
		//	{
		//		var userId = await usersRepository.GetUserIdByRefreshToken(refreshToken);
		//		return new ServiceResult<int>(userId, null);
		//	}
		//	catch (Exception exception)
		//	{
		//		return new ServiceResult<int>(null, new UnauthorizedException(exception.Message));
		//	}
		//}
	}
}
