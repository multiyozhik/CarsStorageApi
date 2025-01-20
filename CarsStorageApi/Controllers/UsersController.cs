using CarsStorage.BLL.Abstractions;
using CarsStorageApi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarsStorageApi.Controllers
{
	[ApiController]
	[Authorize(Roles = "admin")]
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

		//ToDo: при регистрации админом сразу задавать пароль, в RegisterUserDTO добавить список ролей, по умолчанию дефолтная роль
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] RegisterUserDTO registerUserDTO)			
		{
			if (string.IsNullOrWhiteSpace(registerUserDTO.UserName)
				|| string.IsNullOrWhiteSpace(registerUserDTO.Email)
				|| string.IsNullOrWhiteSpace(registerUserDTO.Password))
				return BadRequest();

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

