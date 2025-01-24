using AutoMapper;
using CarsStorage.BLL.Abstractions.Interfaces;
using CarsStorage.BLL.Abstractions.Models;
using CarsStorageApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarsStorageApi.Controllers
{
	[ApiController]
	[Authorize(Policy = "RequierManageUsers")]
	[Route("[controller]/[action]")]
	
	public class UsersController(IUsersService usersService, IMapper mapper) : ControllerBase
	{
		[HttpGet]
		public async Task<ActionResult<IEnumerable<UserRequestResponse>>> GetList()
		{
			var serviceResult = await usersService.GetList();

			if (serviceResult.IsSuccess)
				return serviceResult.Result.Select(mapper.Map<UserRequestResponse>).ToList();
			else
				return BadRequest(serviceResult.ErrorMessage);
		}


		[HttpGet("{id}")]
		public async Task<ActionResult<UserRequestResponse>> GetById([FromRoute] int id)
		{
			var serviceResult = await usersService.GetById(id);

			if (serviceResult.IsSuccess && serviceResult.Result is not null)
				return mapper.Map<UserRequestResponse>(serviceResult.Result);
			else
				return BadRequest(serviceResult.ErrorMessage);
		}


		[HttpPost]
		public async Task<ActionResult<UserRequestResponse>> Create([FromBody] UserRequest userRequest)			
		{
			var serviceResult = await usersService.Create(mapper.Map<AppUserCreaterDTO>(userRequest));

			if (serviceResult.IsSuccess && serviceResult.Result is not null)
				return mapper.Map<UserRequestResponse>(serviceResult.Result);
			else
				return BadRequest(serviceResult.ErrorMessage);
		}


		[HttpPut]
		public async Task<ActionResult<UserRequestResponse>> Update([FromBody] UserRequestResponse userDTO)
		{
			var serviceResult = await usersService.Update(mapper.Map<AppUserDTO>(userDTO));

			if (serviceResult.IsSuccess && serviceResult.Result is not null)
				return mapper.Map<UserRequestResponse>(serviceResult.Result);
			else
				return BadRequest(serviceResult.ErrorMessage);
		}


		[HttpDelete("{id}")]
		public async Task<ActionResult<int>> Delete([FromRoute] int id)
		{
			var serviceResult = await usersService.Delete(id);

			if (serviceResult.IsSuccess)
				return serviceResult.Result;
			else
				return BadRequest(serviceResult.ErrorMessage);
		}

		private ActionResult<T> ReturnActionResult<T>(ServiceResult<T> serviceResult)
		{
			if (serviceResult.IsSuccess && serviceResult.Result is not null)
				return serviceResult.Result;
			else
				return BadRequest(serviceResult.ErrorMessage);
		}
	}
}

