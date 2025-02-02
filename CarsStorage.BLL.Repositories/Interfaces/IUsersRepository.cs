using CarsStorage.BLL.Abstractions.Models;
using CarsStorage.DAL.Entities;
using CarsStorage.DAL.Models;

namespace CarsStorage.BLL.Repositories.Interfaces
{
	public interface IUsersRepository
	{
		public Task<List<IdentityAppUser>> GetList();
		public Task<IdentityAppUser> GetById(int id);
		public Task<IdentityAppUser> Create(IdentityAppUserCreater identityAppUserCreater);
		public Task<IdentityAppUser> Update(IdentityAppUser identityAppUser);
		public Task Delete(int id);
		public Task UpdateToken(string id, JWTTokenDTO jwtTokenDTO);
		public Task<AppUserDTO> GetUserByRefreshToken(string refreshToken);
		public Task<AppUserDTO> ClearToken(JWTTokenDTO jwtTokenDTO);
	}
}
