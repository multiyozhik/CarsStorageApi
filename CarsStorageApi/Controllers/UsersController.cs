﻿using CarsStorage.BLL.Abstractions;
using CarsStorage.BLL.Config;
using CarsStorageApi.Config;
using CarsStorageApi.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CarsStorageApi.Controllers
{
	[ApiController]
	[Authorize(Roles = "admin")]
	[Route("[controller]/[action]")]
	
	public class UsersController(IUsersService usersService, IOptions<RoleNamesConfig> roleNamesConfig) : ControllerBase
	{
		private readonly UserMapper userMapper = new();

		[HttpGet]
		public async Task<IEnumerable<UserDTO>> GetList()
		{
			var appUsersList = await usersService.GetList();
			return appUsersList.Select(userMapper.AppUserToUserDto);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<UserDTO>> GetById([FromRoute] Guid id)
		{
			var appUser = await usersService.GetById(id);
			if (appUser.Value is not null)
				return userMapper.AppUserToUserDto(appUser.Value);
			return NotFound("Пользователь не найден"); 
		}


		[HttpPost]
		public async Task<IActionResult> Create([FromBody] RegisterUserDTO registerUserDTO)			
		{
			if (string.IsNullOrWhiteSpace(registerUserDTO.UserName)
				|| string.IsNullOrWhiteSpace(registerUserDTO.Email)
				|| string.IsNullOrWhiteSpace(registerUserDTO.Password))
				return BadRequest("Ошибка ввода данных пользователя");

			if (roleNamesConfig.Value.DefaultUserRoleNames is null)
				throw new Exception("Не заданы конфигурации, значение роли пользователя по умолчанию");
			else
				registerUserDTO.Roles ??= roleNamesConfig.Value.DefaultUserRoleNames;

			return await usersService.Create(userMapper.RegUserDtoToRegAppUser(registerUserDTO));
		}


		[HttpPut]
		public async Task<IActionResult> Update([FromBody] UserDTO userDTO)
		{
			return await usersService.Update(userMapper.UserDtoToAppUser(userDTO));
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete([FromRoute] Guid id)
		{
			return await usersService.Delete(id);
		}
	}
}

