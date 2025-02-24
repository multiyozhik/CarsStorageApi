using AutoMapper;
using CarsStorage.Abstractions.BLL.Services;
using CarsStorage.Abstractions.DAL.Repositories;
using CarsStorage.Abstractions.Exceptions;
using CarsStorage.Abstractions.General;
using CarsStorage.Abstractions.ModelsDTO.Car;
using CarsStorage.DAL.Entities;

namespace CarsStorage.BLL.Services.Services
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
				var carEntityList = await carsRepository.GetList();
				var carsDTOList = carEntityList.Select(mapper.Map<CarDTO>).ToList();
				return new ServiceResult<List<CarDTO>>(carsDTOList);
			}
			catch (Exception exception)
			{
				return new ServiceResult<List<CarDTO>>(new NotFoundException(exception.Message));
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
				return new ServiceResult<CarDTO>(mapper.Map<CarDTO>(carEntity));
			}
			catch (Exception exception)
			{
				return new ServiceResult<CarDTO>(new BadRequestException(exception.Message));
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
				return new ServiceResult<CarDTO>(mapper.Map<CarDTO>(carEntity));
			}
			catch (Exception exception)
			{
				return new ServiceResult<CarDTO>(new BadRequestException(exception.Message));
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
				return new ServiceResult<int>(id);
			}
			catch (Exception exception)
			{
				return new ServiceResult<int>(new BadRequestException(exception.Message));
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
				return new ServiceResult<CarDTO>(mapper.Map<CarDTO>(carEntity));
			}
			catch (Exception exception)
			{
				return new ServiceResult<CarDTO>(new BadRequestException(exception.Message));
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
				return new ServiceResult<CarDTO>(mapper.Map<CarDTO>(carEntity));
			}
			catch (Exception exception)
			{
				return new ServiceResult<CarDTO>(new BadRequestException(exception.Message));
			}
		}
	}
}
