﻿using CarsStorage.BLL.Abstractions.ModelsDTO.AuthModels;
using CarsStorage.BLL.Abstractions.ModelsDTO.UserDTO;
using CarsStorage.DAL.Entities;
using CarsStorage.DAL.Models;

namespace CarsStorage.BLL.Repositories.Interfaces
{
    public interface IUsersRepository
	{
		public Task<List<UserEntity>> GetList();
		public Task<UserEntity> GetById(int id);
		public Task<UserEntity> FindByName(string userName);
		public Task<UserRegister> Create(UserCreater userCreater);
		public Task<UserEntity> Update(UserEntity userEntity);
		public Task Delete(int id);
		public Task UpdateToken(int id, JWTTokenDTO jwtTokenDTO);
		public Task<UserDTO> GetUserByRefreshToken(string refreshToken);
		public Task<UserDTO> ClearToken(JWTTokenDTO jwtTokenDTO);
	}
}
