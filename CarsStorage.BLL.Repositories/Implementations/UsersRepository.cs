using AutoMapper;
using CarsStorage.BLL.Abstractions.ModelsDTO.AuthModels;
using CarsStorage.BLL.Abstractions.ModelsDTO.UserDTO;
using CarsStorage.BLL.Repositories.Interfaces;
using CarsStorage.DAL.EF;
using CarsStorage.DAL.Entities;
using CarsStorage.DAL.Models;
using CarsStorage.DAL.Utils;
using Microsoft.EntityFrameworkCore;

namespace CarsStorage.BLL.Repositories.Implementations
{
	public class UsersRepository(UsersRolesDbContext dbContext, IPasswordHasher passwordHasher, IMapper mapper) : IUsersRepository
	{
		public async Task<List<UserEntity>> GetList()
		{
			return await dbContext.Users.ToListAsync();
		}


		public async Task<UserEntity> GetById(int id)
		{
			var userEntity = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
			return userEntity is not null ? userEntity : throw new Exception("Не найден пользователь по id");
		}


		public async Task<UserEntity> FindByName(string userName)
		{
			var userEntity = await dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);
			return userEntity is not null ? userEntity : throw new Exception("Не найден пользователь по логину");
		}


		public async Task<UserRegister> Create(UserCreater userCreater)
		{
			var rolesListNames = userCreater.Roles.ToList();

			var rolesList = rolesListNames.Select(roleName => new RoleEntity(roleName)).ToList();

			var userEntity = new UserEntity
			{
				UserName = userCreater.UserName,
				Email = userCreater.Email,
				PasswordHash = passwordHasher.HashPassword(userCreater.Password),
				RolesList = rolesList
			};

			var user = await dbContext.Users.AddAsync(userEntity);
			await dbContext.SaveChangesAsync(); 

			return mapper.Map<UserRegister>(user);
		}


		public async Task<UserEntity> Update(UserEntity user)
		{
			var userEntity = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id)
				?? throw new Exception("Пользователь с заданным Id не найден");
			userEntity.UserName = user.UserName;
			userEntity.Email = user.Email;
			userEntity.RolesList = user.RolesList;
				
			dbContext.Users.Update(userEntity);
			await dbContext.SaveChangesAsync();
			return user;
		}


		public async Task Delete(int id)
		{
			var userEntity = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id)
				?? throw new Exception("Пользователь с заданным Id не найден");
			dbContext.Users.Remove(userEntity);
			await dbContext.SaveChangesAsync();
		}


		public async Task UpdateToken(int id, JWTTokenDTO jwtTokenDTO)
		{
			var userEntity = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id)
				?? throw new Exception("Пользователь с заданным Id не найден");
			userEntity.RefreshToken = jwtTokenDTO.RefreshToken;
			userEntity.AccessToken = jwtTokenDTO.AccessToken;
			dbContext.Update(userEntity);
			await dbContext.SaveChangesAsync();
		}


		public async Task<UserDTO> GetUserByRefreshToken(string refreshToken)
		{
			var user = await dbContext.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
			return mapper.Map<UserDTO>(user);
		}

		public async Task<UserDTO> ClearToken(JWTTokenDTO jwtTokenDTO)
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
