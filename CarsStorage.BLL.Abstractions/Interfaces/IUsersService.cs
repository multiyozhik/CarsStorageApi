using CarsStorage.BLL.Abstractions.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarsStorage.BLL.Abstractions.Interfaces
{
    public interface IUsersService
    {
        public Task<IEnumerable<AppUserDTO>> GetList();
        public Task<ActionResult<AppUserDTO>> GetById(int id);
        public Task<IActionResult> Create(AppUserCreaterDTO appUserCreaterDTO);
        public Task<IActionResult> Update(AppUserDTO appUser);
        public Task<IActionResult> Delete(int id);
    }
}
