using AutoMapper;
using CarsStorage.Abstractions.BLL.Services;
using CarsStorage.Abstractions.ModelsDTO.Location;
using CarsStorageApi.Filters;
using CarsStorageApi.Models.LocationModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CarsStorageApi.Controllers
{
	/// <summary>
	/// Класс контроллера для определения локации пользователя.
	/// </summary>
	/// <param name="locationService">Объект сервиса для определения локации пользователя.</param>
	/// <param name="mapper">Объект меппера.</param>
	/// <param name="logger">Объект для логирования.</param>
	[ApiController]
	[AllowAnonymous]
	[Route("[controller]/[action]")]
	[ServiceFilter(typeof(AcceptHeaderActionFilter))]
	public class LocationController(ILocationService locationService, IMapper mapper, ILogger<LocationController> logger) : ControllerBase
	{
		/// <summary>
		/// Метод определения локации пользователя.
		/// </summary>
		/// <param name="coordinatesHeader">Строка объекта координат пользователя.</param>
		/// <returns>Объект локации пользователя.</returns>
		[HttpGet]
		public async Task<ActionResult<LocationDTO>> GetUserLocation([FromHeader(Name = "Coordinates")] string coordinatesHeader)
		{
			//{"lat": 55.601983, "lon": 37.359486, "radius_meters": 50}
			try
			{
				var coordinateRequest = JsonSerializer.Deserialize<CoordinateRequest>(coordinatesHeader);
				var coordinateDTO = mapper.Map<CoordinateDTO>(coordinateRequest);
				var locationServiceResult = await locationService.GetUserLocation(coordinateDTO);
				if (!locationServiceResult.IsSuccess)
					throw locationServiceResult.ServiceError;
				return locationServiceResult.Result;
			}
			catch (Exception exception)
			{
				logger.LogError("Ошибка в {controller} в методе {method} при определении локации пользователя: {errorMessage}", this, nameof(this.GetUserLocation), exception.Message);
				throw;
			}
		}
	}
}
