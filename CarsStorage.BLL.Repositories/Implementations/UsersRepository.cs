using AutoMapper;
using CarsStorage.BLL.Abstractions.ModelsDTO.AuthModels;
using CarsStorage.BLL.Repositories.Interfaces;
using CarsStorage.BLL.Repositories.Utils;
using CarsStorage.DAL.DbContexts;
using CarsStorage.DAL.Entities;
using CarsStorage.DAL.Models;

using Microsoft.EntityFrameworkCore;

namespace CarsStorage.BLL.Repositories.Implementations
{
	/// <summary>
	/// Класс репозитория для пользователей.
	/// </summary>
	public class UsersRepository(AppDbContext dbContext, IPasswordHasher passwordHasher, IMapper mapper) : IUsersRepository
	{
		/// <summary>
		/// Метод возвращает список всех сущностей пользователей.
		public async Task<List<UserEntity>> GetList()
			=> await dbContext.Users.ToListAsync();


		/// <summary>
		/// Метод возвращает сущность пользователя по полученному id пользователя.
		/// </summary>
		public async Task<UserEntity> GetById(int id)
		{
			var userEntity = await dbContext.Users.FirstOrDefaultAsync(u => u.UserEntityId == id);
			return userEntity is not null ? userEntity : throw new Exception("Не найден пользователь по id");
		}


		/// <summary>
		/// Метод возвращает сущность пользователя по полученному имени пользователя.
		/// </summary>
		public async Task<UserEntity> FindByName(string userName)
		{
			var userEntity = await dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);
			return userEntity is not null ? userEntity : throw new Exception("Не найден пользователь по логину");
		}


		/// <summary>
		/// Метод создает сущность пользователя и возвращает данные пользователя с ролями без пароля.
		/// </summary>
		public async Task<UserRegister> Create(UserCreater userCreater)
		{
			var rolesListNames = userCreater.Roles.ToList();
			var rolesList = rolesListNames.Select(roleName => new RoleEntity(roleName)).ToList();

			var password = passwordHasher.HashPassword(userCreater.Password);

			var userEntity = new UserEntity
			{
				UserName = userCreater.UserName,
				Email = userCreater.Email,
				Hash = password.Hash,
				Salt = password.Salt,
				RolesList = rolesList
			};

			var user = await dbContext.Users.AddAsync(userEntity);
			await dbContext.SaveChangesAsync(); 

			return mapper.Map<UserRegister>(user);
		}


		/// <summary>
		/// Метод изменяет сущность пользователя и возвращает ее.
		/// </summary>
		public async Task<UserEntity> Update(UserEntity user)
		{
			var userEntity = await dbContext.Users.FirstOrDefaultAsync(u => u.UserEntityId == user.UserEntityId)
				?? throw new Exception("Пользователь с заданным Id не найден");
			userEntity.UserName = user.UserName;
			userEntity.Email = user.Email;
			userEntity.RolesList = user.RolesList;
				
			dbContext.Users.Update(userEntity);
			await dbContext.SaveChangesAsync();
			return user;
		}


		/// <summary>
		/// Метод удаляет сущность пользователя по id.
		/// </summary>
		public async Task Delete(int id)
		{
			var userEntity = await dbContext.Users.FirstOrDefaultAsync(u => u.UserEntityId == id)
				?? throw new Exception("Пользователь с заданным Id не найден");
			dbContext.Users.Remove(userEntity);
			await dbContext.SaveChangesAsync();
		}


		/// <summary>
		/// Метод изменяет токен для пользователя с id.
		/// </summary>
		public async Task UpdateToken(int id, JWTTokenDTO jwtTokenDTO)
		{
			var userEntity = await dbContext.Users.FirstOrDefaultAsync(u => u.UserEntityId == id)
				?? throw new Exception("Пользователь с заданным Id не найден");
			userEntity.RefreshToken = jwtTokenDTO.RefreshToken;
			userEntity.AccessToken = jwtTokenDTO.AccessToken;
			dbContext.Update(userEntity);
			await dbContext.SaveChangesAsync();
		}

		/// <summary>
		/// Метод возвращает пользователя по полученному refresh-токену.
		/// </summary>
		public async Task<UserEntity> GetUserByRefreshToken(string refreshToken)
			=> await dbContext.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

		/// <summary>
		/// Метод очищает токен в БД и возвращает пользователя, который вышел из системы.
		/// </summary>
		public async Task<UserEntity> ClearToken(JWTTokenDTO jwtTokenDTO)
		{
			var user = await GetUserByRefreshToken(jwtTokenDTO.RefreshToken)
				?? throw new Exception("Не найден пользователь по id");
			user.RefreshToken = string.Empty;
			user.AccessToken = string.Empty;
			dbContext.Update(user);
			await dbContext.SaveChangesAsync();
			return user;
		}
	}
}
