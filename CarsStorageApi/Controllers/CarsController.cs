using Microsoft.AspNetCore.Mvc;
using CarsStorage.BLL.Interfaces;
using CarsStorage.BLL;
using System.Linq;

namespace CarsStorageApi.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class CarsController : ControllerBase
	{
		private ICarsService carsService;
		private CarMapper carMapper = new CarMapper();

		public CarsController(ICarsService carsService)
		{
			this.carsService = carsService;
		}

		[HttpGet]
		public async Task<IEnumerable<Car>> GetCars()
		{
			var carDTOList = await carsService.GetCarsList();
			return carDTOList.Select(carMapper.CarDTOToCar);
		}

		[HttpPost]
		public async Task Add([FromBody] Car car)
		{
			await carsService.AddAsync(carMapper.CarToCarDTO(car));
		}

		[HttpPut]
		public async Task Update([FromBody] Car car)
		{
			await carsService.UpdateAsync(carMapper.CarToCarDTO(car));
		}

		[HttpDelete]
		public async Task Delete([FromRoute] Guid id)
		{
			await carsService.DeleteAsync(id);
		}
	}
}
