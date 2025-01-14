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
	}
}
