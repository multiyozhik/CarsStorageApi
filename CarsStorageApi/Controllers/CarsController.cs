using AutoMapper;
using CarsStorage.BLL.Abstractions.Interfaces;
using CarsStorage.BLL.Abstractions.Models;
using CarsStorageApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarsStorageApi.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	
	public class CarsController(ICarsService carsService, IMapper mapper) : ControllerBase
	{
		[Authorize(Policy = "RequierBrowseCars")]
		[HttpGet]
		public async Task<ActionResult<IEnumerable<CarRequestResponse>>> GetCars()
		{
			var serviceResult = await carsService.GetList();

			if (serviceResult.IsSuccess)
				return serviceResult.Result.Select(mapper.Map<CarRequestResponse>).ToList();
			else
				return BadRequest(serviceResult.ErrorMessage);			
		}


		[Authorize(Policy = "RequierManageCars")]
		[HttpPost]
		public async Task<ActionResult<CarDTO>> Create([FromBody] CarRequest carRequest)
		{
			var serviceResult = await carsService.Create(mapper.Map<CarCreaterDTO>(carRequest));
			return ReturnActionResult(serviceResult);
		}


		[Authorize(Policy = "RequierManageCars")]
		[HttpPut]
		public async Task<ActionResult<CarDTO>> Update([FromBody] CarRequestResponse carRequestResponse)
		{
			var serviceResult = await carsService.Update(mapper.Map<CarDTO>(carRequestResponse));
			return ReturnActionResult(serviceResult);
		}


		[Authorize(Policy = "RequierManageCars")]
		[HttpDelete("{id}")]
		public async Task<ActionResult<int>> Delete([FromRoute] int id)
		{
			var serviceResult = await carsService.Delete(id);
			return ReturnActionResult(serviceResult);
		}


		[Authorize(Policy = "RequierManageCars")]
		[HttpPut]
		public async Task<ActionResult<CarDTO>> UpdateCount([FromBody] CarCountChangerRequest carCountChangerRequest)
		{
			var serviceResult = await carsService.UpdateCount(carCountChangerRequest.Id, carCountChangerRequest.Count);
			return ReturnActionResult(serviceResult);
		}


		private ActionResult<T> ReturnActionResult<T>(ServiceResult<T> serviceResult)
		{
			if (serviceResult.IsSuccess && serviceResult.Result is not null)
				return serviceResult.Result;
			else
				return BadRequest(serviceResult.ErrorMessage);
		}
	}
}
