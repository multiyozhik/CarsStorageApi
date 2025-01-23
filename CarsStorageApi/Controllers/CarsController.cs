using CarsStorage.BLL.Abstractions;
using CarsStorage.BLL.Abstractions.Interfaces;
using CarsStorageApi.Mappers;
using CarsStorageApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarsStorageApi.Controllers
{
    [ApiController]
	[Route("[controller]/[action]")]
	
	public class CarsController(ICarsService carsService) : ControllerBase
	{
		private readonly CarMapper carMapper = new();

		[Authorize(Policy = "RequierBrowseCars")]
		[HttpGet]
		public async Task<IEnumerable<CarRequestResponse>> GetCars()
		{
			var carList = await carsService.GetList();
			return carList.Select(carMapper.CarDtoToCarRequestResponse);
		}

		[Authorize(Policy = "RequierManageCars")]
		[HttpPost]
		public async Task Create([FromBody] CarRequest carRequest)
		{
			await carsService.Create(carMapper.CarRequestToCarCreaterDTO(carRequest));
		}

		[Authorize(Policy = "RequierManageCars")]
		[HttpPut]
		public async Task Update([FromBody] CarRequestResponse carRequestResponse)
		{
			await carsService.Update(carMapper.CarRequestResponseToCarDTO(carRequestResponse));
		}

		[Authorize(Policy = "RequierManageCars")]
		[HttpDelete("{id}")]
		public async Task Delete([FromRoute] int id)
		{
			await carsService.Delete(id);
		}

		[Authorize(Policy = "RequierManageCars")]
		[HttpPut]
		public async Task UpdateCount([FromRoute] int id, [FromQuery] int count)
		{
			await carsService.UpdateCount(id, count);
		}
	}
}
