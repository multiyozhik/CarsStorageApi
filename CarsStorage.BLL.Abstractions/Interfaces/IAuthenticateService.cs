using CarsStorage.BLL.Abstractions.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarsStorage.BLL.Abstractions.Interfaces
{
    public interface IAuthenticateService
    {
        public Task<ServiceResult<AppUserDTO>> Register(AppUserRegisterDTO registerAppUser);
        public Task<ServiceResult<JWTTokenDTO>> LogIn(AppUserLoginDTO appUserLoginDTO);
        public Task LogOut();
    }
}
