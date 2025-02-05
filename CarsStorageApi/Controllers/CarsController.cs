using AutoMapper;
using CarsStorage.BLL.Abstractions.Interfaces;
using CarsStorage.BLL.Abstractions.ModelsDTO.CarDTO;
using CarsStorageApi.Models.CarModels;
using CarsStorageApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarsStorageApi.Controllers
{
	/// <summary>
	/// ����� ����������� ��� ����������.
	/// </summary>
	[ApiController]
	[Route("[controller]/[action]")]	
	public class CarsController(ICarsService carsService, IMapper mapper, HttpContext httpContext) : ControllerBase
	{
		/// <summary>
		/// ����� ���������� ������ � ������� ���� �����������.
		/// </summary>
		[Authorize(Policy = "RequierBrowseCars")]
		[Authorize(Policy = "RequierManageCars")]
		[HttpGet]
		public async Task<ActionResult<List<CarRequestResponse>>> GetCars(HttpContext httpContext)
		{
			var serviceResult = await carsService.GetList();
			if (serviceResult.IsSuccess)
			{
				var carsList = serviceResult.Result.Select(mapper.Map<CarRequestResponse>).ToList();
				return (httpContext.User.HasClaim(c => c.Value == "RequierBrowseCars"))
					? carsList.Where(c => c.IsAccassible).ToList()
					: carsList;
			}
			return ExceptionHandler.HandleException(this, serviceResult.ServiceError);
		}


		/// <summary>
		/// ����� ���������� ������ � ��������� ������� ����������.
		/// </summary>
		[Authorize(Policy = "RequierManageCars")]
		[HttpPost]
		public async Task<ActionResult<CarRequestResponse>> Create([FromBody] CarRequest carRequest)
		{
			var serviceResult = await carsService.Create(mapper.Map<CarCreaterDTO>(carRequest));
			
			if (serviceResult.IsSuccess)
				return mapper.Map<CarRequestResponse>(serviceResult.Result);
			return ExceptionHandler.HandleException(this, serviceResult.ServiceError);
		}

		/// <summary>
		/// ����� ���������� ������ � ���������� ������� ����������.
		/// </summary>
		[Authorize(Policy = "RequierManageCars")]
		[HttpPut]
		public async Task<ActionResult<CarRequestResponse>> Update([FromBody] CarRequestResponse carRequestResponse)
		{
			var serviceResult = await carsService.Update(mapper.Map<CarDTO>(carRequestResponse));

			if (serviceResult.IsSuccess)
				return mapper.Map<CarRequestResponse>(serviceResult.Result);
			return ExceptionHandler.HandleException(this, serviceResult.ServiceError);
		}

		/// <summary>
		/// ����� ���������� ������ � id ��������� ������ ����������.
		/// </summary>
		[Authorize(Policy = "RequierManageCars")]
		[HttpDelete("{id}")]
		public async Task<ActionResult<int>> Delete([FromRoute] int id)
		{
			var serviceResult = await carsService.Delete(id);

			if (serviceResult.IsSuccess)
				return mapper.Map<int>(serviceResult.Result);
			return ExceptionHandler.HandleException(this, serviceResult.ServiceError);
		}

		/// <summary>
		/// ����� ���������� ������ � ���������� ������� ���������� (�������� ���������� �����������).
		/// </summary>
		[Authorize(Policy = "RequierManageCars")]
		[HttpPut]
		public async Task<ActionResult<CarRequestResponse>> UpdateCount([FromBody] CarCountChangerRequest carCountChangerRequest)
		{
			var serviceResult = await carsService.UpdateCount(carCountChangerRequest.Id, carCountChangerRequest.Count);

			if (serviceResult.IsSuccess)
				return mapper.Map<CarRequestResponse>(serviceResult.Result);
			return ExceptionHandler.HandleException(this, serviceResult.ServiceError);
		}

		/// <summary>
		/// ����� ���������� ������ � ���������� ������� ���������� (������ ������� ����������� ��� ��������� ������� �������������).
		/// </summary>
		[Authorize(Policy = "RequierManageCars")]
		[HttpPut]
		public async Task<ActionResult<CarRequestResponse>> MakeInaccessible([FromRoute]int id)
		{
			var serviceResult = await carsService.MakeInaccessible(id);

			if (serviceResult.IsSuccess)
				return mapper.Map<CarRequestResponse>(serviceResult.Result);
			return ExceptionHandler.HandleException(this, serviceResult.ServiceError);
		}
	}
}
