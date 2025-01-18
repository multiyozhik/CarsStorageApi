using CarsStorage.BLL.Interfaces;
using CarsStorageApi.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarsStorageApi.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	
	public class CarsController(ICarsService carsService) : ControllerBase
	{
		private readonly ICarsService carsService = carsService;
		private readonly CarMapper carMapper = new();

		[Authorize(Roles = "manager,user")]
		[HttpGet]
		public async Task<IEnumerable<CarDTO>> GetCars()
		{
			var carList = await carsService.GetList();
			return carList.Select(carMapper.CarToCarDto);
		}

		[Authorize(Roles = "manager")]
		[HttpPost]
		public async Task Add([FromBody] CarDTO carDTO)
		{
			await carsService.AddAsync(carMapper.CarDtoToCar(carDTO));
		}

		[Authorize(Roles = "manager")]
		[HttpPut]
		public async Task Update([FromBody] CarDTO carDTO)
		{
			await carsService.UpdateAsync(carMapper.CarDtoToCar(carDTO));
		}

		[Authorize(Roles = "manager")]
		[HttpDelete("{id}")]
		public async Task Delete([FromRoute] Guid id)
		{
			await carsService.DeleteAsync(id);
		}

		[Authorize(Roles = "manager")]
		[HttpPut]
		public async Task UpdateCount([FromRoute] Guid id, [FromQuery] int count)
		{
			await carsService.UpdateCount(id, count);
		}
	}
}
