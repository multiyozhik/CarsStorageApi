﻿using AutoMapper;
using CarsStorage.Abstractions.DAL.Repositories;
using CarsStorage.Abstractions.ModelsDTO.User;
using CarsStorage.BLL.Abstractions.ModelsDTO.User;
using CarsStorage.DAL.DbContexts;
using CarsStorage.DAL.Entities;
using CarsStorage.DAL.Repositories.Utils;
using Microsoft.EntityFrameworkCore;
using Riok.Mapperly.Abstractions;

namespace CarsStorage.DAL.Repositories.Implementations
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
			var userEntityList = await dbContext.Users.Include(u => u.RolesList).ToListAsync();
			return userEntityList.Select(mapper.Map<UserDTO>).ToList();
		}


		/// <summary>
		/// Метод возвращает dto-пользователя по id пользователя.
		/// </summary>
		public async Task<UserDTO> GetById(int id)
		{
			var userEntity = await dbContext.Users.Include(u => u.RolesList).FirstOrDefaultAsync(u => u.UserEntityId == id);
			return userEntity is not null 
				? mapper.Map<UserDTO>(userEntity) 
				: throw new Exception("Не найден пользователь по id");
		}


		/// <summary>
		/// Метод возвращает dto-пользователя по username пользователя.
		/// </summary>
		public async Task<UserDTO> GetUserByAuthUserData(AuthUserData authUserData)
		{
			var userEntity = await dbContext.Users.Include(u => u.RolesList).FirstOrDefaultAsync(u => u.UserName == authUserData.UserName);
			if (userEntity is null)
			{
				userEntity = new UserEntity()
				{
					UserName = authUserData.UserName,
					Email = authUserData.Email,
					AccessToken = authUserData.AccessTokenFromAuthService,
					RolesList = await dbContext.Roles.Where(r => authUserData.RolesNamesList.Contains(r.Name)).ToListAsync()
				};
				await dbContext.Users.AddAsync(userEntity);
				await dbContext.SaveChangesAsync();
			}						
			return mapper.Map<UserDTO>(userEntity);
		}


		/// <summary>
		/// Метод проверяет валидность логина и пароля в БД пользователя.
		/// </summary>
		public async Task IsUserValid(UserLoginDTO userLoginDTO)
		{
			var userEntity = await dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userLoginDTO.UserName)
				?? throw new Exception("Неверный логин.");
			if (string.IsNullOrEmpty(userEntity.Hash) || string.IsNullOrEmpty(userEntity.Salt))
				throw new Exception("Не определены пароль и соль.");
			if (!passwordHasher.VerifyPassword(userLoginDTO.Password, userEntity.Hash, userEntity.Salt))
				throw new Exception("Неверный пароль.");			
		}

		/// <summary>
		/// Метод возвращает найденного в БД пользователя с его ролями.
		/// </summary>
		public async Task<UserDTO> GetUserWithRoles(UserLoginDTO userLoginDTO)
		{
			var userEntity = await dbContext.Users.Include(u => u.RolesList).FirstOrDefaultAsync(u => u.UserName == userLoginDTO.UserName);				
			return mapper.Map<UserDTO>(userEntity);
		}


		/// <summary>
		/// Метод возвращает созданного пользователя в БД (пароль хешируется перед сохранением в БД).
		/// </summary>
		public async Task<UserDTO> Create(UserCreaterDTO userCreaterDTO)
		{
			var userEntity = mapper.Map<UserEntity>(userCreaterDTO);
			var hashedPassword = passwordHasher.HashPassword(userCreaterDTO.Password);
			userEntity.Hash = hashedPassword.Hash;
			userEntity.Salt = hashedPassword.Salt;
			if (userCreaterDTO.RoleNamesList is null)
				throw new Exception("Не определены роли пользователя.");
			userEntity.RolesList = await dbContext.Roles.Where(r => userCreaterDTO.RoleNamesList.Contains(r.Name)).ToListAsync();
			await dbContext.Users.AddAsync(userEntity);
			await dbContext.SaveChangesAsync();
			return mapper.Map<UserDTO>(userEntity);
		}


		///// <summary>
		///// Метод возвращает созданного пользователя в БД, аутентентифицированного в сервисе аутентификации.
		///// </summary>
		//public async Task<UserDTO> CreateAuthUser(UserCreaterDTO userCreaterDTO)
		//{
		//	var userEntity = mapper.Map<UserEntity>(userCreaterDTO);			
		//	if (userCreaterDTO.RoleNamesList is null)
		//		throw new Exception("Не определены роли пользователя.");
		//	userEntity.RolesList = await dbContext.Roles.Where(r => userCreaterDTO.RoleNamesList.Contains(r.Name)).ToListAsync();
		//	await dbContext.Users.AddAsync(userEntity);
		//	await dbContext.SaveChangesAsync();
		//	return mapper.Map<UserDTO>(userEntity);
		//}



		/// <summary>
		/// Метод возвращает обновленного пользователя из БД.
		/// </summary>
		public async Task<UserDTO> Update(UserUpdaterDTO userUpdaterDTO)
		{
			var userEntity = await dbContext.Users.FirstOrDefaultAsync(u => u.UserEntityId == userUpdaterDTO.Id)
				?? throw new Exception("Пользователь с заданным Id не найден");			
			userEntity.UserName = userUpdaterDTO.UserName;
			userEntity.Email = userUpdaterDTO.Email;
			if (userUpdaterDTO.RoleNamesList is null)
				throw new Exception("Не определены роли пользователя.");
			userEntity.RolesList = await dbContext.Roles.Where(r => userUpdaterDTO.RoleNamesList.Contains(r.Name)).ToListAsync();
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
	}
}
