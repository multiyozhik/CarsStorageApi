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
			var userEntityList = await dbContext.Users.Include(u => u.RolesList).ToListAsync();
			return userEntityList.Select(mapper.Map<UserDTO>).ToList();
		}


		/// <summary>
		/// Метод возвращает по id пользователя.
		/// </summary>
		public async Task<UserDTO> GetById(int id)
		{
			var userEntity = await dbContext.Users.Include(u => u.RolesList).FirstOrDefaultAsync(u => u.UserEntityId == id);
			return userEntity is not null 
				? mapper.Map<UserDTO>(userEntity) 
				: throw new Exception("Не найден пользователь по id");
		}

		/// <summary>
		/// Метод проверяет валидность логина и пароля и возвращает найденного в БД пользователя.
		/// </summary>
		public async Task<UserDTO> GetUserIfValid(UserLoginDTO userLoginDTO)
		{
			var userEntity = await dbContext.Users.Include(u => u.RolesList).FirstOrDefaultAsync(u => u.UserName == userLoginDTO.UserName)
				?? throw new Exception("Неверный логин.");
			if (!passwordHasher.VerifyPassword(userLoginDTO.Password, userEntity.Hash, userEntity.Salt))
				throw new Exception("Неверный пароль.");				
			return mapper.Map<UserDTO>(userEntity);
		}


		/// <summary>
		/// Метод возвращает созданного администратором пользователя в БД.
		/// </summary>
		public async Task<UserDTO> Create(UserCreaterDTO userCreaterDTO)
		{
			var userEntity = mapper.Map<UserEntity>(userCreaterDTO);
			await dbContext.Users.AddAsync(userEntity);
			await dbContext.SaveChangesAsync();
			return mapper.Map<UserDTO>(userEntity);
		}


		/// <summary>
		/// Метод возвращает зарегистрированного пользователя в БД пользователя с передачей дефолтного списка ролей (пароль хешируется перед сохранением в БД).
		/// </summary>
		public async Task<UserDTO> Register(UserCreaterDTO userCreaterDTO, IEnumerable<string> rolesNamesList)
		{
			var userEntity = mapper.Map<UserEntity>(userCreaterDTO);
			userEntity.RolesList = await dbContext.Roles.Where(r => rolesNamesList.Contains(r.Name)).ToListAsync();
			var hashedPassword = passwordHasher.HashPassword(userCreaterDTO.Password);
			userEntity.Hash = hashedPassword.Hash;
			userEntity.Salt = hashedPassword.Salt;
			var user = await dbContext.Users.AddAsync(userEntity);
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
		public async Task<JWTTokenDTO> UpdateToken(int userId, JWTTokenDTO jwtTokenDTO)
		{
			var userEntity = await dbContext.Users.FirstOrDefaultAsync(u => u.UserEntityId == userId)
				?? throw new Exception("Пользователь с заданным Id не найден");
			userEntity.AccessToken = jwtTokenDTO.AccessToken;
			userEntity.RefreshToken = jwtTokenDTO.RefreshToken;
			dbContext.Update(userEntity);
			await dbContext.SaveChangesAsync();
			return new JWTTokenDTO { AccessToken= jwtTokenDTO.AccessToken , RefreshToken= jwtTokenDTO.RefreshToken };
		}


		/// <summary>
		/// Метод очищает токен в БД при выходе пользователя из системы.
		/// </summary>
		public async Task<int> ClearToken(string accessToken)
		{
			var userEntity = await dbContext.Users.FirstOrDefaultAsync(u => u.AccessToken == accessToken) 
				?? throw new Exception("Неверный токен");
			userEntity.RefreshToken = null;
			userEntity.AccessToken = null;
			dbContext.Update(userEntity);
			await dbContext.SaveChangesAsync();
			return userEntity.UserEntityId;
		}
	}
}
