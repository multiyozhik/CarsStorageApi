using AutoMapper;
using CarsStorage.Abstractions.BLL.Services;
using CarsStorage.Abstractions.ModelsDTO.Car;
using CarsStorageApi.Models.CarModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarsStorageApi.Controllers
{
	/// <summary>
	/// Класс контроллера для автомобилей.
	/// </summary>
	/// <param name="carsService">Объект сервиса автомобилей.</param>
	/// <param name="mapper">Объект меппера.</param>
	[ApiController]
	[Route("[controller]/[action]")]
	public class CarsController(ICarsService carsService, IMapper mapper) : ControllerBase
	{
		/// <summary>
		/// Метод возвращает список автомобилей.
		/// </summary>
		/// <returns>Список объектов автомобилей, возвращаемый клиенту.</returns>
		[Authorize(Policy = "RequierBrowseCars")]
		[HttpGet]
		public async Task<ActionResult<List<CarResponse>>> GetList()
		{
			var serviceResult = await carsService.GetList();
			if (serviceResult.IsSuccess)
			{
				var carsList = serviceResult.Result.Select(mapper.Map<CarResponse>).ToList();
				return (HttpContext.User.HasClaim(c => c.Value == "RequierBrowseCars"))
					? carsList.Where(c => c.IsAccassible).ToList()
					: carsList;
			}
			else
				throw serviceResult.ServiceError;
		}


		/// <summary>
		/// Метод для создания объекта автомобиля.
		/// </summary>
		/// <param name="carRequest">Объект данных автомобиля, передаваемых клиентом.</param>
		/// <returns>Созданный объект данных автомобиля, возвращаемых клиенту.</returns>
		[Authorize(Policy = "RequierManageCars")]
		[HttpPost]
		public async Task<ActionResult<CarResponse>> Create([FromBody] CarRequest carRequest)
		{
			var serviceResult = await carsService.Create(mapper.Map<CarCreaterDTO>(carRequest));
			
			if (serviceResult.IsSuccess)
				return mapper.Map<CarResponse>(serviceResult.Result);
			else
				throw serviceResult.ServiceError;
		}


		/// <summary>
		/// Метод для изменения объекта автомобиля.
		/// </summary>
		/// <param name="carResponse">Объект автомобиля.</param>
		/// <returns>Измененный объект данных автомобиля, возвращаемых клиенту.</returns>
		[Authorize(Policy = "RequierManageCars")]
		[HttpPut]
		public async Task<ActionResult<CarResponse>> Update([FromBody] CarResponse carResponse)
		{
			var serviceResult = await carsService.Update(mapper.Map<CarDTO>(carResponse));

			if (serviceResult.IsSuccess)
				return mapper.Map<CarResponse>(serviceResult.Result);
			throw serviceResult.ServiceError;
		}


		/// <summary>
		/// Метод для удаления объекта автомобиля по его идентификатору.
		/// </summary>
		/// <param name="id">Идентификатор объекта автомобиля.</param>
		/// <returns>Идентификатор удаленного объекта автомобиля.</returns>
		[Authorize(Policy = "RequierManageCars")]
		[HttpDelete]
		public async Task<ActionResult<int>> Delete([FromQuery] int id)
		{
			var serviceResult = await carsService.Delete(id);

			if (serviceResult.IsSuccess)
				return mapper.Map<int>(serviceResult.Result);
			throw serviceResult.ServiceError;
		}


		/// <summary>
		/// Метод для изменения количества у объекта автомобиля с идентификатором id.
		/// </summary>
		/// <param name="carCountRequest">Объект данных автомобиля, передаваемых клиентом.</param>
		/// <returns>Измененный объект автомобиля, возвращаемый клиенту.</returns>
		[Authorize(Policy = "RequierManageCars")]
		[HttpPut]
		public async Task<ActionResult<CarResponse>> UpdateCount([FromBody] CarCountRequest carCountRequest)
		{
			var serviceResult = await carsService.UpdateCount(carCountRequest.Id, carCountRequest.Count);

			if (serviceResult.IsSuccess)
				return mapper.Map<CarResponse>(serviceResult.Result);
			else
				return BadRequest(serviceResult.ServiceError);
		}


		/// <summary>
		/// Метод для изменения объекта автомобиля так, чтобы объект был недоступен для просмотра (обычным пользователем).
		/// </summary>
		/// <param name="id">Идентификатор объекта автомобиля.</param>
		/// <returns>Измененный объект автомобиля, возвращаемый клиенту.</returns>
		[Authorize(Policy = "RequierManageCars")]
		[HttpPut]
		public async Task<ActionResult<CarResponse>> MakeInaccessible([FromQuery]int id)
		{
			var serviceResult = await carsService.MakeInaccessible(id);

			if (serviceResult.IsSuccess)
				return mapper.Map<CarResponse>(serviceResult.Result);
			throw serviceResult.ServiceError;
		}
	}
}
