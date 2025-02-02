using CarsStorage.BLL.Abstractions.Models;

namespace CarsStorage.BLL.Abstractions.Interfaces
{
	public interface IAuthenticateService
    {
        public Task<ServiceResult<AppUserDTO>> Register(AppUserRegisterDTO appUserRegisterDTO);
        public Task<ServiceResult<JWTTokenDTO>> LogIn(AppUserLoginDTO appUserLoginDTO);
        public Task<ServiceResult<JWTTokenDTO>> RefreshToken(JWTTokenDTO jwtTokenDTO);
		public Task<AppUserDTO> LogOut(JWTTokenDTO jwtTokenDTO);
    }
}
