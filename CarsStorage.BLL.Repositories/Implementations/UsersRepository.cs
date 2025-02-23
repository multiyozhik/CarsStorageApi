using CarsStorage.Abstractions.DAL.Repositories;
using CarsStorage.DAL.DbContexts;
using CarsStorage.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarsStorage.DAL.Repositories.Implementations
{
	/// <summary>
	/// Класс репозитория для пользователей (использует сущность пользователя и возвращает данные пользователя без пароля).
	/// </summary>
	public class UsersRepository(AppDbContext dbContext) : IUsersRepository
	{
		/// <summary>
		/// Метод возвращает список всех пользователей из БД.
		public async Task<List<UserEntity>> GetList()
		{
			return await dbContext.Users.Include(u => u.RolesList).ToListAsync();
		}


		/// <summary>
		/// Метод возвращает dto-пользователя по id пользователя.
		/// </summary>
		public async Task<UserEntity> GetUserByUserId(int id)
		{
			return await dbContext.Users.Include(u => u.RolesList).FirstOrDefaultAsync(u => u.UserEntityId == id)
				?? throw new Exception("Не найден пользователь по id");
		}


		/// <summary>
		/// Метод возвращает dto-пользователя по id пользователя.
		/// </summary>
		public async Task<UserEntity?> GetUserByUserName(string userName)
		{
			return await dbContext.Users.Include(u => u.RolesList).FirstOrDefaultAsync(u => u.UserName == userName);
		}


		/// <summary>
		/// Метод возвращает список сущностей ролей по списку имен ролей для пользователя.
		/// </summary>
		public async Task<List<RoleEntity>> GetRolesByRoleNames(List<string> roleNamesList)
		{
			return await dbContext.Roles.Where(r => roleNamesList.Contains(r.Name)).ToListAsync();
		}


		/// <summary>
		/// Метод возвращает пользователя по refresh токену.
		/// </summary>
		public async Task<UserEntity> GetUserByRefreshToken(string refreshToken)
		{
			return await dbContext.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken)
				?? throw new Exception("Пользователь с заданным токеном не найден");
		}


		/// <summary>
		/// Метод возвращает созданного пользователя в БД (пароль хешируется перед сохранением в БД).
		/// </summary>
		public async Task<UserEntity> Create(UserEntity userEntity)
		{			
			await dbContext.Users.AddAsync(userEntity);
			await dbContext.SaveChangesAsync();
			return userEntity;
		}


		/// <summary>
		/// Метод возвращает обновленного пользователя из БД.
		/// </summary>
		public async Task<UserEntity> Update(UserEntity userEntity)
		{
			var user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserEntityId == userEntity.UserEntityId)
				?? throw new Exception("Пользователь с заданным Id не найден");
			user.UserName = userEntity.UserName;
			user.Email = userEntity.Email;
			user.RolesList = userEntity.RolesList;
			dbContext.Users.Update(user);
			await dbContext.SaveChangesAsync();
			return user;
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
	}
}
