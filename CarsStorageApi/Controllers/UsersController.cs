using AutoMapper;
using CarsStorage.BLL.Abstractions.Services;
using CarsStorage.BLL.Abstractions.ModelsDTO.User;
using CarsStorageApi.Models.UserModels;
using CarsStorageApi.Utils;
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
	public class UsersController(IUsersService usersService, IRolesService rolesService, IMapper mapper) : ControllerBase
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
			return ExceptionHandler.HandleException(this, serviceResult.ServiceError);
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
			return ExceptionHandler.HandleException(this, serviceResult.ServiceError);
		}

		/// <summary>
		/// Метод возвращает задачу с созданным пользователем.
		/// </summary>
		[HttpPost]
		public async Task<ActionResult<UserResponse>> Create([FromBody] UserRequest userRequest)			
		{
			var rolesServiceResult = await rolesService.GetRolesByNamesList(userRequest.Roles);
			if (!rolesServiceResult.IsSuccess)
				return ExceptionHandler.HandleException(this, rolesServiceResult.ServiceError);
			var userCreaterDTO = mapper.Map<UserCreaterDTO>(userRequest);
			userCreaterDTO.RolesList = rolesServiceResult.Result;
			var serviceResult = await usersService.Create(userCreaterDTO);
			if (serviceResult.IsSuccess)
				return mapper.Map<UserResponse>(serviceResult.Result);
			return ExceptionHandler.HandleException(this, serviceResult.ServiceError);
		}

		/// <summary>
		/// Метод возвращает задачу с измененным пользователем.
		/// </summary>
		[HttpPut]
		public async Task<ActionResult<UserResponse>> Update([FromBody] UserResponse userResponse)
		{
			var serviceResult = await usersService.Update(mapper.Map<UserDTO>(userResponse));

			if (serviceResult.IsSuccess)
				return mapper.Map<UserResponse>(serviceResult.Result);
			return ExceptionHandler.HandleException(this, serviceResult.ServiceError);
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
			return ExceptionHandler.HandleException(this, serviceResult.ServiceError);
		}
	}
}

