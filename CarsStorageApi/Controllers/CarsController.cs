using CarsStorage.BLL.Abstractions;
using CarsStorage.BLL.Interfaces;
using CarsStorageApi.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarsStorageApi.Controllers
{
	[ApiController]
	[Authorize]
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

		[Authorize(Roles = "manager,user")]
		[HttpPost]
		public async Task Create([FromBody] CreaterCarDTO createrCarDTO)
		{
			var car = new Car()
			{
				Id = Guid.NewGuid(),
				Make = createrCarDTO.Make,
				Model = createrCarDTO.Model,
				Color = createrCarDTO.Color,
				Count = createrCarDTO.Count
			};
			await carsService.Create(car);
		}

		[Authorize(Roles = "manager")]
		[HttpPut]
		public async Task Update([FromBody] CarDTO carDTO)
		{
			await carsService.Update(carMapper.CarDtoToCar(carDTO));
		}

		[Authorize(Roles = "manager")]
		[HttpDelete("{id}")]
		public async Task Delete([FromRoute] Guid id)
		{
			await carsService.Delete(id);
		}

		[Authorize(Roles = "manager")]
		[HttpPut]
		public async Task UpdateCount([FromRoute] Guid id, [FromQuery] int count)
		{
			await carsService.UpdateCount(id, count);
		}
	}
}
