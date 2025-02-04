using AutoMapper;
using CarsStorage.BLL.Abstractions.Interfaces;
using CarsStorage.BLL.Abstractions.ModelsDTO.UserDTO;
using CarsStorageApi.Models.UserModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarsStorageApi.Controllers
{
	/// <summary>
	/// Класс контролллера пользователей.
	/// </summary>
    [ApiController]
	[Authorize(Policy = "RequierManageUsers")]
	[Route("[controller]/[action]")]	
	public class UsersController(IUsersService usersService, IMapper mapper) : ControllerBase
	{
		/// <summary>
		/// Метод возвращает задачу с списком всех пользователей.
		/// </summary>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<UserRequestResponse>>> GetList()
		{
			var serviceResult = await usersService.GetList();

			if (serviceResult.IsSuccess)
				return serviceResult.Result.Select(mapper.Map<UserRequestResponse>).ToList();
			else
				return BadRequest(serviceResult.ErrorMessage);
		}

		/// <summary>
		/// Метод возвращает задачу с пользователем по его id.
		/// </summary>
		[HttpGet("{id}")]
		public async Task<ActionResult<UserRequestResponse>> GetById([FromRoute] int id)
		{
			var serviceResult = await usersService.GetById(id);

			if (serviceResult.IsSuccess)
				return mapper.Map<UserRequestResponse>(serviceResult.Result);
			else
				return BadRequest(serviceResult.ErrorMessage);
		}

		/// <summary>
		/// Метод возвращает задачу с созданным пользователем.
		/// </summary>
		[HttpPost]
		public async Task<ActionResult<UserRequestResponse>> Create([FromBody] UserRequest userRequest)			
		{
			var serviceResult = await usersService.Create(mapper.Map<UserCreaterDTO>(userRequest));

			if (serviceResult.IsSuccess)
				return mapper.Map<UserRequestResponse>(serviceResult.Result);
			else
				return BadRequest(serviceResult.ErrorMessage);
		}

		/// <summary>
		/// Метод возвращает задачу с измененным пользователем.
		/// </summary>
		[HttpPut]
		public async Task<ActionResult<UserRequestResponse>> Update([FromBody] UserRequestResponse userRequestResponse)
		{
			var serviceResult = await usersService.Update(mapper.Map<UserDTO>(userRequestResponse));

			if (serviceResult.IsSuccess)
				return mapper.Map<UserRequestResponse>(serviceResult.Result);
			else
				return BadRequest(serviceResult.ErrorMessage);
		}

		/// <summary>
		/// Метод возвращает задачу с id удаленного пользователя.
		/// </summary>
		[HttpDelete("{id}")]
		public async Task<ActionResult<int>> Delete([FromRoute] int id)
		{
			var serviceResult = await usersService.Delete(id);

			if (serviceResult.IsSuccess)
				return serviceResult.Result;
			else
				return BadRequest(serviceResult.ErrorMessage);
		}
	}
}

