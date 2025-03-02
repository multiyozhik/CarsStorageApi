using AutoMapper;
using CarsStorage.Abstractions.BLL.Services;
using CarsStorage.Abstractions.ModelsDTO.User;
using CarsStorageApi.Filters;
using CarsStorageApi.Models.UserModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarsStorageApi.Controllers
{
	/// <summary>
	/// Класс контроллера пользователей.
	/// </summary>
	/// <param name="usersService">Сервис пользователей.</param>
	/// <param name="mapper">Объект меппера.</param>
	/// <param name="logger">Объект для выполнения логирования.</param>
	[ApiController]
	[Authorize(Policy = "RequierManageUsers")]
	[Route("[controller]/[action]")]
	[ServiceFilter(typeof(AcceptHeaderActionFilter))]
	public class UsersController(IUsersService usersService, IMapper mapper, ILogger<UsersController> logger) : ControllerBase
	{
		/// <summary>
		/// Метод возвращает список всех пользователей.
		/// </summary>
		/// <returns>Список всех пользователей.</returns>
		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<UserResponse>), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IEnumerable<UserResponse>>> GetList()
		{
			try
			{
				var serviceResult = await usersService.GetList();

				if (serviceResult.IsSuccess)
					return serviceResult.Result.Select(mapper.Map<UserResponse>).ToList();
				throw serviceResult.ServiceError;
			}
			catch (Exception exception)
			{
				logger.LogError("Ошибка в {controller} в методе {method} при получении списка пользователей: {errorMessage}", this, nameof(this.GetList), exception.Message);
				throw;
			}
		}


		/// <summary>
		/// Метод возвращает пользователя по его идентификатору.
		/// </summary>
		/// <param name="id">Идентификатор пользователя.</param>
		/// <returns>Объект пользователя, возвращаемый клиенту.</returns>
		[HttpGet("{id}")]
		[ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<UserResponse>> GetById([FromRoute] int id)
		{
			try
			{
				var serviceResult = await usersService.GetById(id);

				if (serviceResult.IsSuccess)
					return mapper.Map<UserResponse>(serviceResult.Result);
				throw serviceResult.ServiceError;
			}
			catch (Exception exception)
			{
				logger.LogError("Ошибка в {controller} в методе {method} при получении пользователя по его идентификатору: {errorMessage}", this, nameof(this.GetById), exception.Message);
				throw;
			}
		}


		/// <summary>
		/// Метод для создания объекта пользователя.
		/// </summary>
		/// <param name="userRequest">Объект данных пользователя, передаваемых клиентом при создании нового пользователя с ролью.</param>
		/// <returns>Созданный объект пользователя, возвращаемый клиенту.</returns>
		[HttpPost]
		[ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		public async Task<ActionResult<UserResponse>> Create([FromBody] UserRequest userRequest)			
		{
			try
			{
				var userCreaterDTO = mapper.Map<UserCreaterDTO>(userRequest);
				var serviceResult = await usersService.Create(userCreaterDTO);
				if (serviceResult.IsSuccess)
					return mapper.Map<UserResponse>(serviceResult.Result);
				throw serviceResult.ServiceError;
			}
			catch (Exception exception)
			{
				logger.LogError("Ошибка в {controller} в методе {method} при создании объекта пользователя: {errorMessage}", this, nameof(this.Create), exception.Message);
				throw;
			}
		}


		/// <summary>
		/// Метод для изменения объекта пользователя.
		/// </summary>
		/// <param name="userResponse">Объект пользователя.</param>
		/// <returns>Измененный объект пользователя, возвращаемый клиенту.</returns>
		[HttpPut]
		[ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<UserResponse>> Update([FromBody] UserResponse userResponse)
		{
			try
			{
				var serviceResult = await usersService.Update(mapper.Map<UserUpdaterDTO>(userResponse));

				if (serviceResult.IsSuccess)
					return mapper.Map<UserResponse>(serviceResult.Result);
				throw serviceResult.ServiceError;
			}
			catch (Exception exception)
			{
				logger.LogError("Ошибка в {controller} в методе {method} при изменении объекта пользователя: {errorMessage}", this, nameof(this.Update), exception.Message);
				throw;
			}
		}


		/// <summary>
		/// Метод для удаления пользователя по его идентификатору.
		/// </summary>
		/// <param name="id">Идентификатор пользователя.</param>
		/// <returns>Идентификатор удаленного  пользователя.</returns>
		[HttpDelete("{id}")]
		[ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status403Forbidden)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<int>> Delete([FromRoute] int id)
		{
			try
			{
				var serviceResult = await usersService.Delete(id);

				if (serviceResult.IsSuccess)
					return serviceResult.Result;
				throw serviceResult.ServiceError;
			}
			catch (Exception exception)
			{
				logger.LogError("Ошибка в {controller} в методе {method} при удалении пользователя по его идентификатору: {errorMessage}", this, nameof(this.Delete), exception.Message);
				throw;
			}
		}
	}
}

