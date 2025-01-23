using CarsStorage.BLL.Abstractions.Interfaces;
using CarsStorageApi.Config;
using CarsStorageApi.Mappers;
using CarsStorageApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CarsStorageApi.Controllers
{
    [ApiController]
	[Authorize(Policy = "RequierManageUsers")]
	[Route("[controller]/[action]")]
	
	public class UsersController(IUsersService usersService) : ControllerBase
	{
		private readonly UserMapper userMapper = new();

		[HttpGet]
		public async Task<IEnumerable<UserRequestResponse>> GetList()
		{
			var appUsersList = await usersService.GetList();
			return appUsersList.Select(userMapper.AppUserDtoToUserRequestResponse);
		}


		[HttpGet("{id}")]
		public async Task<ActionResult<UserRequestResponse>> GetById([FromRoute] int id)
		{
			var appUserDTO = await usersService.GetById(id);
			return userMapper.AppUserDtoToUserRequestResponse(appUserDTO.Value);
		}


		[HttpPost]
		public async Task<IActionResult> Create([FromBody] UserRequest userRequest)			
		{
			return await usersService.Create(userMapper.UserRequestToAppUserCreaterDto(userRequest));
		}


		[HttpPut]
		public async Task<IActionResult> Update([FromBody] UserRequestResponse userDTO)
		{
			return await usersService.Update(userMapper.UserRequestResponseToAppUserDto(userDTO));
		}


		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			return await usersService.Delete(id);
		}
	}
}

