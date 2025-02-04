﻿using CarsStorage.BLL.Abstractions.Models;
using CarsStorage.BLL.Abstractions.ModelsDTO.AuthModels;
using CarsStorage.BLL.Abstractions.ModelsDTO.UserDTO;

namespace CarsStorage.BLL.Abstractions.Interfaces
{
	public interface IAuthenticateService
	{
		public Task<ServiceResult<UserDTO>> Register(UserRegisterDTO userRegisterDTO);
		public Task<ServiceResult<JWTTokenDTO>> LogIn(UserLoginDTO userLoginDTO);
		public Task<ServiceResult<JWTTokenDTO>> RefreshToken(JWTTokenDTO jwtTokenDTO);
		public Task<UserDTO> LogOut(JWTTokenDTO jwtTokenDTO);
	}
}
