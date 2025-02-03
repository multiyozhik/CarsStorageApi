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
		public async Task<ActionResult<List<CarRequestResponse>>> GetCars()
		{
			var serviceResult = await carsService.GetList();

			if (serviceResult.IsSuccess)
				return serviceResult.Result.Select(mapper.Map<CarRequestResponse>).ToList();
			else
				return BadRequest(serviceResult.ErrorMessage);			
		}


		[Authorize(Policy = "RequierManageCars")]
		[HttpPost]
		public async Task<ActionResult<CarRequestResponse>> Create([FromBody] CarRequest carRequest)
		{
			var serviceResult = await carsService.Create(mapper.Map<CarCreaterDTO>(carRequest));
			
			if (serviceResult.IsSuccess && serviceResult.Result is not null)
				return mapper.Map<CarRequestResponse>(serviceResult.Result);
			else
				return BadRequest(serviceResult.ErrorMessage);
		}


		[Authorize(Policy = "RequierManageCars")]
		[HttpPut]
		public async Task<ActionResult<CarRequestResponse>> Update([FromBody] CarRequestResponse carRequestResponse)
		{
			var serviceResult = await carsService.Update(mapper.Map<CarDTO>(carRequestResponse));

			if (serviceResult.IsSuccess && serviceResult.Result is not null)
				return mapper.Map<CarRequestResponse>(serviceResult.Result);
			else
				return BadRequest(serviceResult.ErrorMessage);
		}


		[Authorize(Policy = "RequierManageCars")]
		[HttpDelete("{id}")]
		public async Task<ActionResult<int>> Delete([FromRoute] int id)
		{
			var serviceResult = await carsService.Delete(id);

			if (serviceResult.IsSuccess)
				return mapper.Map<int>(serviceResult.Result);
			else
				return BadRequest(serviceResult.ErrorMessage);
		}


		[Authorize(Policy = "RequierManageCars")]
		[HttpPut]
		public async Task<ActionResult<CarRequestResponse>> UpdateCount([FromBody] CarCountChangerRequest carCountChangerRequest)
		{
			var serviceResult = await carsService.UpdateCount(carCountChangerRequest.Id, carCountChangerRequest.Count);

			if (serviceResult.IsSuccess && serviceResult.Result is not null)
				return mapper.Map<CarRequestResponse>(serviceResult.Result);
			else
				return BadRequest(serviceResult.ErrorMessage);
		}


		//private ActionResult<T> ReturnActionResult<T>(ServiceResult<T> serviceResult)
		//{
		//	if (serviceResult.IsSuccess && serviceResult.Result is not null)
		//		return mapper.Map<T>(serviceResult.Result);
		//	else
		//		return BadRequest(serviceResult.ErrorMessage);
		//}
	}
}
