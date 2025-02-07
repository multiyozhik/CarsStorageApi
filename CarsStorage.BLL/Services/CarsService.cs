using AutoMapper;
using CarsStorage.BLL.Abstractions.Exceptions;
using CarsStorage.BLL.Abstractions.Interfaces;
using CarsStorage.BLL.Abstractions.Models;
using CarsStorage.BLL.Abstractions.ModelsDTO.CarDTO;
using CarsStorage.BLL.Abstractions.ModelsDTO.UserDTO;
using CarsStorage.BLL.Repositories.Interfaces;
using CarsStorage.DAL.Entities;

namespace CarsStorage.BLL.Implementations.Services
{
	/// <summary>
	/// Класс сервиса автомобилей.
	/// </summary>
	public class CarsService(ICarsRepository carsRepository, IMapper mapper) : ICarsService
	{
		/// <summary>
		/// Метод возвращает как результат список всех автомобилей.
		/// </summary>
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
				return new ServiceResult<List<CarDTO>>(null, new NotFoundException(exception.Message));
			}			
		}

		/// <summary>
		/// Метод создает новую запись автомобиля и возвращает как результат созданный объект автомобиля.
		/// </summary>
		public async Task<ServiceResult<CarDTO>> Create(CarCreaterDTO carCreaterDTO)
		{
			try
			{
				var carEntity = await carsRepository.Create(mapper.Map<CarEntity>(carCreaterDTO));
				return new ServiceResult<CarDTO>(mapper.Map<CarDTO>(carEntity), null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<CarDTO>(null, new BadRequestException(exception.Message));
			}
		}

		/// <summary>
		/// Метод изменяет данные об автомобиле и возвращает как результат измененный объект автомобиля.
		/// </summary>
		public async Task<ServiceResult<CarDTO>> Update(CarDTO carDTO)
		{
			try
			{
				var carEntity = await carsRepository.Update(mapper.Map<CarEntity>(carDTO));
				return new ServiceResult<CarDTO>(mapper.Map<CarDTO>(carEntity), null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<CarDTO>(null, new BadRequestException(exception.Message));
			}
		}

		/// <summary>
		/// Метод удаляет запись об автомобиле по его id и возвращает как результат этот id.
		/// </summary>
		public async Task<ServiceResult<int>> Delete(int id)
		{
			try
			{
				await carsRepository.Delete(id);
				return new ServiceResult<int>(id, null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<int>(id, new BadRequestException(exception.Message));
			}
		}

		/// <summary>
		/// Метод изменяет количество автомобилей и возвращает как результат измененный объект автомобиля.
		/// </summary>
		public async Task<ServiceResult<CarDTO>> UpdateCount(int id, int count)
		{
			try
			{
				var carEntity = await carsRepository.UpdateCount(id, count);
				return new ServiceResult<CarDTO>(mapper.Map<CarDTO>(carEntity), null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<CarDTO>(null, new BadRequestException(exception.Message));
			}
		}

		/// <summary>
		/// Метод делает запись об автомобиле недоступной по id автомобиля и возвращает как результат измененный объект автомобиля.
		/// </summary>
		public async Task<ServiceResult<CarDTO>> MakeInaccessible(int id)
		{
			try
			{
				var carEntity = await carsRepository.MakeInaccessible(id);
				return new ServiceResult<CarDTO>(mapper.Map<CarDTO>(carEntity), null);
			}
			catch (Exception exception)
			{
				return new ServiceResult<CarDTO>(null, new BadRequestException(exception.Message));
			}
		}
	}
}
