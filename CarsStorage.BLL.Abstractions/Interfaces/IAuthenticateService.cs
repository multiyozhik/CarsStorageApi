using CarsStorage.BLL.Abstractions.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarsStorage.BLL.Abstractions.Interfaces
{
    public interface IAuthenticateService
    {
        public Task<IActionResult> Register(AppUserRegisterDTO registerAppUser);
        public Task<ActionResult<JWTTokenDTO>> LogIn(AppUserLoginDTO appUserLoginDTO);
        public Task LogOut();
    }
}
