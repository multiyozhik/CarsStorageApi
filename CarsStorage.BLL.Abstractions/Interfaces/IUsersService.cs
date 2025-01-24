using CarsStorage.BLL.Abstractions.Models;

namespace CarsStorage.BLL.Abstractions.Interfaces
{
	public interface IUsersService
    {
        public Task<ServiceResult<IEnumerable<AppUserDTO>>> GetList();
        public Task<ServiceResult<AppUserDTO>> GetById(int id);
        public Task<ServiceResult<AppUserDTO>> Create(AppUserCreaterDTO appUserCreaterDTO);
        public Task<ServiceResult<AppUserDTO>> Update(AppUserDTO appUserDTO);
        public Task<ServiceResult<int>> Delete(int id);
    }
}
