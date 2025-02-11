using AutoMapper;
using CarsStorage.BLL.Abstractions.Services;
using CarsStorage.BLL.Abstractions.ModelsDTO.Car;
using CarsStorageApi.Models.CarModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarsStorageApi.Controllers
{
	/// <summary>
	/// ����� ����������� ��� ����������.
	/// </summary>
	[ApiController]
	[Route("[controller]/[action]")]
	public class CarsController(ICarsService carsService, IMapper mapper) : ControllerBase
	{
		/// <summary>
		/// ����� ���������� ������ � ������� ���� �����������.
		/// </summary>
		[Authorize(Policy = "RequierBrowseCars")]
		[HttpGet]
		public async Task<ActionResult<List<CarResponse>>> GetList()
		{
			var serviceResult = await carsService.GetList();
			if (serviceResult.IsSuccess)
			{
				var carsList = serviceResult.Result.Select(mapper.Map<CarResponse>).ToList();
				return (HttpContext.User.HasClaim(c => c.Value == "RequierBrowseCars"))
					? carsList.Where(c => c.IsAccassible).ToList()
					: carsList;
			}
			else
				return BadRequest(serviceResult.ServiceError);			
		}


		/// <summary>
		/// ����� ���������� ������ � ��������� ������� ����������.
		/// </summary>
		[Authorize(Policy = "RequierManageCars")]
		[HttpPost]
		public async Task<ActionResult<CarResponse>> Create([FromBody] CarRequest carRequest)
		{
			var serviceResult = await carsService.Create(mapper.Map<CarCreaterDTO>(carRequest));
			
			if (serviceResult.IsSuccess)
				return mapper.Map<CarResponse>(serviceResult.Result);
			else
				return BadRequest(serviceResult.ServiceError);
		}

		/// <summary>
		/// ����� ���������� ������ � ���������� ������� ����������.
		/// </summary>
		[Authorize(Policy = "RequierManageCars")]
		[HttpPut]
		public async Task<ActionResult<CarResponse>> Update([FromBody] CarResponse carResponse)
		{
			var serviceResult = await carsService.Update(mapper.Map<CarDTO>(carResponse));

			if (serviceResult.IsSuccess)
				return mapper.Map<CarResponse>(serviceResult.Result);
			else
				return BadRequest(serviceResult.ServiceError);
		}

		/// <summary>
		/// ����� ���������� ������ � id ��������� ������ ����������.
		/// </summary>
		[Authorize(Policy = "RequierManageCars")]
		[HttpDelete]
		public async Task<ActionResult<int>> Delete([FromQuery] int id)
		{
			var serviceResult = await carsService.Delete(id);

			if (serviceResult.IsSuccess)
				return mapper.Map<int>(serviceResult.Result);
			else
				return BadRequest(serviceResult.ServiceError);
		}

		/// <summary>
		/// ����� ���������� ������ � ���������� ������� ���������� (�������� ���������� �����������).
		/// </summary>
		[Authorize(Policy = "RequierManageCars")]
		[HttpPut]
		public async Task<ActionResult<CarResponse>> UpdateCount([FromBody] CarCountRequest carCountRequest)
		{
			var serviceResult = await carsService.UpdateCount(carCountRequest.Id, carCountRequest.Count);

			if (serviceResult.IsSuccess)
				return mapper.Map<CarResponse>(serviceResult.Result);
			else
				return BadRequest(serviceResult.ServiceError);
		}

		/// <summary>
		/// ����� ���������� ������ � ���������� ������� ���������� (������ ������� ����������� ��� ��������� ������� �������������).
		/// </summary>
		[Authorize(Policy = "RequierManageCars")]
		[HttpPut]
		public async Task<ActionResult<CarResponse>> MakeInaccessible([FromQuery]int id)
		{
			var serviceResult = await carsService.MakeInaccessible(id);

			if (serviceResult.IsSuccess)
				return mapper.Map<CarResponse>(serviceResult.Result);
			else
				return BadRequest(serviceResult.ServiceError);
		}
	}
}
