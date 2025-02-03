using CarsStorage.BLL.Abstractions.Models;
using CarsStorage.DAL.Entities;
using CarsStorage.DAL.Models;

namespace CarsStorage.BLL.Repositories.Interfaces
{
	public interface IUsersRepository
	{
		public Task<List<AppUserEntity>> GetList();
		public Task<AppUserEntity> GetById(int id);
		public Task<AppUserEntity> Create(IdentityAppUserCreater identityAppUserCreater);
		public Task<AppUserEntity> Update(AppUserEntity identityAppUser);
		public Task Delete(int id);
		public Task UpdateToken(string id, JWTTokenDTO jwtTokenDTO);
		public Task<AppUserDTO> GetUserByRefreshToken(string refreshToken);
		public Task<AppUserDTO> ClearToken(JWTTokenDTO jwtTokenDTO);
	}
}
