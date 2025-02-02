using AutoMapper;
using CarsStorage.BLL.Abstractions.Interfaces;
using CarsStorage.BLL.Abstractions.Models;
using CarsStorage.BLL.Repositories.Interfaces;

namespace CarsStorage.BLL.Implementations.Services
{
	/// <summary>
	/// Сервис для выполнения CRUD-операций для автомобилей.
	/// </summary>
	/// <param name="carsRepository"></param>

	public class CarsService(ICarsRepository carsRepository, IMapper mapper) : ICarsService
	{
		public async Task<ServiceResult<List<CarDTO>>> GetList()
		{
			try
			{
				var carsEntityList = await carsRepository.GetList();
				var carsDTOList = carsEntityList.Select(mapper.Map<CarDTO>).ToList();
				return new ServiceResult<List<CarDTO>>(carsDTOList, null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<List<CarDTO>>(null, exception.Message);
			}			
		}


		public async Task<ServiceResult<CarDTO>> Create(CarCreaterDTO carCreaterDTO)
		{
			try
			{
				var carEntity = await carsRepository.Create(carCreaterDTO);
				return new ServiceResult<CarDTO>(mapper.Map<CarDTO>(carEntity), null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<CarDTO>(null, exception.Message);
			}
		}


		public async Task<ServiceResult<CarDTO>> Update(CarDTO carDTO)
		{
			try
			{
				var carEntity = await carsRepository.Update(carDTO);
				return new ServiceResult<CarDTO>(mapper.Map<CarDTO>(carEntity), null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<CarDTO>(null, exception.Message);
			}
		}


		public async Task<ServiceResult<int>> Delete(int id)
		{
			try
			{
				await carsRepository.Delete(id);
				return new ServiceResult<int>(id, null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<int>(id, exception.Message);
			}
		}


		public async Task<ServiceResult<CarDTO>> UpdateCount(int id, int count)
		{
			try
			{
				var carEntity = await carsRepository.UpdateCount(id, count);
				return new ServiceResult<CarDTO>(mapper.Map<CarDTO>(carEntity), null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<CarDTO>(null, exception.Message);
			}
		}				
	}
}
