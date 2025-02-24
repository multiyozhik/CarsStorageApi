using CarsStorage.Abstractions.DAL.Repositories;
using CarsStorage.DAL.DbContexts;
using CarsStorage.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarsStorage.DAL.Repositories.Implementations
{
	/// <summary>
	/// Класс репозитория пользователей.
	/// </summary>
	/// <param name="dbContext">Объект контекста данных.</param>
	public class UsersRepository(AppDbContext dbContext) : IUsersRepository
	{
		/// <summary>
		/// Метод для получения списка объектов пользователей.
		/// </summary>
		/// <returns>Список объектов пользователей.</returns>
		public async Task<List<UserEntity>> GetList()
		{
			return await dbContext.Users.Include(u => u.RolesList).ToListAsync();
		}


		/// <summary>
		/// Метод для получения объекта пользователя по его идентификатору.
		/// </summary>
		/// <param name="id">Идентификатор пользователя.</param>
		/// <returns>Объект пользователя.</returns>
		public async Task<UserEntity> GetUserByUserId(int id)
		{
			return await dbContext.Users.Include(u => u.RolesList).FirstOrDefaultAsync(u => u.UserEntityId == id)
				?? throw new Exception("Не найден пользователь по id");
		}


		/// <summary>
		/// Метод для получения объекта пользователя по его имени.
		/// </summary>
		/// <param name="userName">Имя пользователя.</param>
		/// <returns>Объект пользователя.</returns>
		public async Task<UserEntity?> GetUserByUserName(string userName)
		{
			return await dbContext.Users.Include(u => u.RolesList).FirstOrDefaultAsync(u => u.UserName == userName);
		}


		/// <summary>
		/// Метод для получения списка объектов ролей по списку наименований этих ролей.
		/// </summary>
		/// <param name="roleNamesList">Список наименований ролей.</param>
		/// <returns></returns>
		public async Task<List<RoleEntity>> GetRolesByRoleNames(List<string> roleNamesList)
		{
			return await dbContext.Roles.Where(r => roleNamesList.Contains(r.Name)).ToListAsync();
		}


		/// <summary>
		/// Метод для получения объекта пользователя по его токену обновления.
		/// </summary>
		/// <param name="refreshToken">Токен обновления для пользователя.</param>
		/// <returns>Объект пользователя.</returns>
		public async Task<UserEntity> GetUserByRefreshToken(string refreshToken)
		{
			return await dbContext.Users.Include(u => u.RolesList).FirstOrDefaultAsync(u => u.RefreshToken == refreshToken)
				?? throw new Exception("Пользователь с заданным токеном не найден");
		}


		/// <summary>
		/// Метод для создания объекта пользователя.
		/// </summary>
		/// <param name="userEntity">Объект пользователя для создания.</param>
		/// <returns>Созданный объект пользователя.</returns>
		public async Task<UserEntity> Create(UserEntity userEntity)
		{			
			await dbContext.Users.AddAsync(userEntity);
			await dbContext.SaveChangesAsync();
			return userEntity;
		}


		/// <summary>
		/// Метод для изменения объекта пользователя.
		/// </summary>
		/// <param name="userEntity">Объект пользователя для изменения.</param>
		/// <returns>Измененный объект пользователя.</returns>
		public async Task<UserEntity> Update(UserEntity userEntity)
		{
			try
			{
				var user = await dbContext.Users.Include(u => u.RolesList).FirstOrDefaultAsync(u => u.UserEntityId == userEntity.UserEntityId)
				?? throw new Exception("Пользователь с заданным Id не найден");
				user.UserName = userEntity.UserName;
				user.Email = userEntity.Email;
				user.RolesList = userEntity.RolesList;
				dbContext.Users.Update(user);
				await dbContext.SaveChangesAsync();
				return user;
			}
			catch (Exception ex)
			{
				var message = ex.Message;
				throw new Exception(message);
			}
		}


		/// <summary>
		/// Метод для удаления объекта пользователя по его идентификатору.
		/// </summary>
		/// <param name="id">Идентификатор пользователя.</param>
		/// <returns>Идентификатор удаленного пользователя.</returns>
		public async Task Delete(int id)
		{
			var userEntity = await dbContext.Users.Include(u => u.RolesList).FirstOrDefaultAsync(u => u.UserEntityId == id)
				?? throw new Exception("Пользователь с заданным Id не найден");
			dbContext.Users.Remove(userEntity);
			await dbContext.SaveChangesAsync();
		}
	}
}
