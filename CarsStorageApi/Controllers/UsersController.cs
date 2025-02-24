using AutoMapper;
using CarsStorage.Abstractions.BLL.Services;
using CarsStorage.Abstractions.ModelsDTO.User;
using CarsStorageApi.Models.UserModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarsStorageApi.Controllers
{
	/// <summary>
	/// Класс контроллера пользователей.
	/// </summary>
	[ApiController]
	[Authorize(Policy = "RequierManageUsers")]
	[Route("[controller]/[action]")]	
	public class UsersController(IUsersService usersService, IMapper mapper) : ControllerBase
	{
		/// <summary>
		/// Метод возвращает список всех пользователей.
		/// </summary>
		/// <returns>Список всех пользователей.</returns>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<UserResponse>>> GetList()
		{
			var serviceResult = await usersService.GetList();

			if (serviceResult.IsSuccess)
				return serviceResult.Result.Select(mapper.Map<UserResponse>).ToList();
			throw serviceResult.ServiceError;
		}


		/// <summary>
		/// Метод возвращает пользователя по его идентификатору.
		/// </summary>
		/// <param name="id">Идентификатор пользователя.</param>
		/// <returns>Объект пользователя, возвращаемый клиенту.</returns>
		[HttpGet("{id}")]
		public async Task<ActionResult<UserResponse>> GetById([FromRoute] int id)
		{
			var serviceResult = await usersService.GetById(id);

			if (serviceResult.IsSuccess)
				return mapper.Map<UserResponse>(serviceResult.Result);
			throw serviceResult.ServiceError;
		}


		/// <summary>
		/// Метод для создания объекта пользователя.
		/// </summary>
		/// <param name="userRequest">Объект данных пользователя, передаваемых клиентом при создании нового пользователя с ролью.</param>
		/// <returns>Созданный объект пользователя, возвращаемый клиенту.</returns>
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
		/// Метод для изменения объекта пользователя.
		/// </summary>
		/// <param name="userResponse">Объект пользователя.</param>
		/// <returns>Измененный объект пользователя, возвращаемый клиенту.</returns>
		[HttpPut]
		public async Task<ActionResult<UserResponse>> Update([FromBody] UserResponse userResponse)
		{
			var serviceResult = await usersService.Update(mapper.Map<UserUpdaterDTO>(userResponse));

			if (serviceResult.IsSuccess)
				return mapper.Map<UserResponse>(serviceResult.Result);
			throw serviceResult.ServiceError;
		}


		/// <summary>
		/// Метод для удаления пользователя по его идентификатору.
		/// </summary>
		/// <param name="id">Идентификатор пользователя.</param>
		/// <returns>Идентификатор удаленного  пользователя.</returns>
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

