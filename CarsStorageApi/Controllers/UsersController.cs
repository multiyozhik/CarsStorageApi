using AutoMapper;
using CarsStorage.BLL.Abstractions.ModelsDTO.User;
using CarsStorage.BLL.Abstractions.Services;
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
		public async Task<ActionResult<IEnumerable<UserResponse>>> GetList()
		{
			var serviceResult = await usersService.GetList();

			if (serviceResult.IsSuccess)
				return serviceResult.Result.Select(mapper.Map<UserResponse>).ToList();
			throw serviceResult.ServiceError;
		}

		/// <summary>
		/// Метод возвращает задачу с пользователем по его id.
		/// </summary>
		[HttpGet("{id}")]
		public async Task<ActionResult<UserResponse>> GetById([FromRoute] int id)
		{
			var serviceResult = await usersService.GetById(id);

			if (serviceResult.IsSuccess)
				return mapper.Map<UserResponse>(serviceResult.Result);
			throw serviceResult.ServiceError;
		}

		/// <summary>
		/// Метод возвращает задачу с созданным пользователем.
		/// </summary>
		[HttpPost]
		public async Task<ActionResult<UserResponse>> Create([FromBody] UserRequest userRequest)			
		{
			var userCreaterDTO = mapper.Map<UserCreaterDTO>(userRequest);
			var serviceResult = await usersService.Create(userCreaterDTO);
			if (serviceResult.IsSuccess)
				return mapper.Map<UserResponse>(serviceResult.Result);
			throw serviceResult.ServiceError;
		}

		/// <summary>
		/// Метод возвращает задачу с измененным пользователем.
		/// </summary>
		[HttpPut]
		public async Task<ActionResult<UserResponse>> Update([FromBody] UserResponse userResponse)
		{
			var serviceResult = await usersService.Update(mapper.Map<UserUpdaterDTO>(userResponse));

			if (serviceResult.IsSuccess)
				return mapper.Map<UserResponse>(serviceResult.Result);
			throw serviceResult.ServiceError;
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
			throw serviceResult.ServiceError;
		}
	}
}

