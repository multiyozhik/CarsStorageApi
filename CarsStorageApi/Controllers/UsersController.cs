﻿using CarsStorage.BLL.Abstractions;
using CarsStorage.BLL.Implementations;
using CarsStorageApi.Filters;
using CarsStorageApi.ModelsDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarsStorageApi.Controllers
{
    [ApiController]
	[Route("[controller]/[action]")]
	[ValidateModel]
	public class UsersController(IUsersService usersService) : ControllerBase
	{
		private readonly UserMapper userMapper = new UserMapper();
		private readonly IUsersService usersService = usersService;

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
			return userMapper.AppUserToUserDto(appUser.Value);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] RegisterUserDTO registerUserDTO)			
		{
			if (string.IsNullOrWhiteSpace(registerUserDTO.UserName)
				|| string.IsNullOrWhiteSpace(registerUserDTO.Email)
				|| string.IsNullOrWhiteSpace(registerUserDTO.Password))
				return BadRequest();

			return await usersService.Create(userMapper.RegUserDtoToRegAppUser(registerUserDTO));
		}

		[HttpPost]
		public async Task<IActionResult> Update([FromBody] UserDTO userDTO)
		{
			return await usersService.Update(userMapper.UserDtoToAppUser(userDTO));
		}

		[HttpPost("{id}")]
		public async Task<IActionResult> Delete([FromRoute] Guid id)
		{
			return await usersService.Delete(id);
		}
	}
}
