using AutoMapper;
using CarsStorage.BLL.Abstractions.ModelsDTO.Token;
using CarsStorage.BLL.Abstractions.ModelsDTO.User;
using CarsStorage.BLL.Abstractions.Repositories;
using CarsStorage.BLL.Repositories.Utils;
using CarsStorage.DAL.DbContexts;
using CarsStorage.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarsStorage.BLL.Repositories.Implementations
{
	/// <summary>
	/// Класс репозитория для пользователей (использует сущность пользователя и возвращает данные пользователя без пароля).
	/// </summary>
	public class UsersRepository(AppDbContext dbContext, IPasswordHasher passwordHasher, IMapper mapper) : IUsersRepository
	{
		/// <summary>
		/// Метод возвращает список всех пользователей из БД.
		public async Task<List<UserDTO>> GetList()
		{
			var userEntityList = await dbContext.Users.ToListAsync();
			return userEntityList.Select(mapper.Map<UserDTO>).ToList();
		}


		/// <summary>
		/// Метод возвращает по id пользователя.
		/// </summary>
		public async Task<UserDTO> GetById(int id)
		{
			var userEntity = await dbContext.Users.FirstOrDefaultAsync(u => u.UserEntityId == id);
			return userEntity is not null 
				? mapper.Map<UserDTO>(userEntity) 
				: throw new Exception("Не найден пользователь по id");
		}


		public async Task<UserDTO> GetUserIfValid(UserLoginDTO userLoginDTO)
		{
			var userEntity = await FindByName(userLoginDTO.UserName) ?? throw new Exception("Неверный логин.");
			if (!passwordHasher.VerifyPassword(userLoginDTO.Password, userEntity.Hash, userEntity.Salt))
				throw new Exception("Неверный пароль.");
			return mapper.Map<UserDTO>(userEntity);
		}


		/// <summary>
		/// Метод возвращает созданного в БД пользователя с ролями.
		/// </summary>
		public async Task<UserDTO> Create(UserCreaterDTO userCreaterDTO)
		{
			var userEntity = mapper.Map<UserEntity>(userCreaterDTO);
			var user = await dbContext.Users.AddAsync(userEntity);
			//ser.Entity
			await dbContext.SaveChangesAsync(); 
			return mapper.Map<UserDTO>(userEntity);
		}


		/// <summary>
		/// Метод возвращает обновленного пользователя из БД.
		/// </summary>
		public async Task<UserDTO> Update(UserDTO userDTO)
		{
			var userEntity = await dbContext.Users.FirstOrDefaultAsync(u => u.UserEntityId == userDTO.Id)
				?? throw new Exception("Пользователь с заданным Id не найден");
			userEntity.UserName = userDTO.UserName;
			userEntity.Email = userDTO.Email;
			if (userDTO.RolesList is null)
				throw new Exception("Не заданы роли пользователя.");
			var rolesEntity = userDTO.RolesList.Select(mapper.Map<RoleEntity>).ToList();				
			userEntity.RolesList = rolesEntity;
			dbContext.Users.Update(userEntity);
			await dbContext.SaveChangesAsync();
			return mapper.Map<UserDTO>(userEntity);
		}


		/// <summary>
		/// Метод удаляет из БД по id пользователя.
		/// </summary>
		public async Task Delete(int id)
		{
			var userEntity = await dbContext.Users.FirstOrDefaultAsync(u => u.UserEntityId == id)
				?? throw new Exception("Пользователь с заданным Id не найден");
			dbContext.Users.Remove(userEntity);
			await dbContext.SaveChangesAsync();
		}

		/// <summary>
		/// Метод возвращает пользователя по refresh токену.
		/// </summary>
		public async Task<UserDTO> GetUserByRefreshToken(string refreshToken)
		{
			var userEntity = await dbContext.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken)
				?? throw new Exception("Пользователь с заданным токеном не найден");
			return mapper.Map<UserDTO>(userEntity);
		}


		/// <summary>
		/// Метод возвращает токен по id пользователя.
		/// </summary>
		public async Task<JWTTokenDTO> GetTokenByUserId(int userId)
		{
			var userEntity = await dbContext.Users.FirstOrDefaultAsync(u => u.UserEntityId == userId)
				?? throw new Exception("Пользователь с заданным id не найден");
			return new JWTTokenDTO { AccessToken = userEntity.AccessToken , RefreshToken = userEntity.RefreshToken};
		}


		/// <summary>
		/// Метод обновляет токен для пользователя с id.
		/// </summary>
		public async Task UpdateToken(int id, JWTTokenDTO jwtTokenDTO)
		{
			var userEntity = await dbContext.Users.FirstOrDefaultAsync(u => u.UserEntityId == id)
				?? throw new Exception("Пользователь с заданным Id не найден");
			userEntity.AccessToken = jwtTokenDTO.AccessToken;
			userEntity.RefreshToken = jwtTokenDTO.RefreshToken;
			dbContext.Update(userEntity);
			await dbContext.SaveChangesAsync();
		}


		/// <summary>
		/// Метод очищает токен в БД при выходе пользователя из системы.
		/// </summary>
		public async Task<int> ClearToken(string refreshToken)
		{
			var userEntity = await dbContext.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken) 
				?? throw new Exception("Неверный токен");
			userEntity.RefreshToken = string.Empty;
			userEntity.AccessToken = string.Empty;
			dbContext.Update(userEntity);
			await dbContext.SaveChangesAsync();
			return userEntity.UserEntityId;
		}


		/// <summary>
		/// Закрытый метод возвращает из БД пользователя по его имени.
		/// </summary>
		private async Task<UserEntity> FindByName(string userName)
		{
			var userEntity = await dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);
			return userEntity is not null
				? userEntity
				: throw new Exception("Пользователь не найден по заданному имени.");
		}
	}
}
