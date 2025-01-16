using Microsoft.AspNetCore.Mvc;
using CarsStorage.BLL.Interfaces;
using CarsStorage.BLL;
using System.Linq;
using CarsStorageApi.Filters;
using Microsoft.AspNetCore.Authorization;

namespace CarsStorageApi.Controllers
{
	[ApiController]
	[Authorize(Roles = "manager")]
	[Route("[controller]/[action]")]
	[ValidateModel]
	public class CarsController(ICarsService carsService) : ControllerBase
	{
		private readonly ICarsService carsService = carsService;
		private readonly CarMapper carMapper = new();

		[Authorize(Roles = "manager, user")]
		[HttpGet]
		public async Task<IEnumerable<CarDTO>> GetCars()
		{
			var carList = await carsService.GetList();
			return carList.Select(carMapper.CarToCarDto);
		}

		[HttpPost]
		public async Task Add([FromBody] CarDTO carDTO)
		{
			await carsService.AddAsync(carMapper.CarDtoToCar(carDTO));
		}

		[HttpPut]
		public async Task Update([FromBody] CarDTO carDTO)
		{
			await carsService.UpdateAsync(carMapper.CarDtoToCar(carDTO));
		}

		[HttpDelete("{id}")]
		public async Task Delete([FromRoute] Guid id)
		{
			await carsService.DeleteAsync(id);
		}

		[HttpPut]
		public async Task ChangeCount([FromRoute] Guid id, [FromQuery] int count)
		{
			await carsService.ChangeCount(id, count);
		}
	}
}
