using AutoMapper;
using CarsStorage.BLL.Abstractions.Exceptions;
using CarsStorage.BLL.Abstractions.General;
using CarsStorage.BLL.Abstractions.Services;
using CarsStorage.BLL.Abstractions.ModelsDTO.Car;
using CarsStorage.BLL.Abstractions.Repositories;

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
				var carsDTOList = await carsRepository.GetList();
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
				var carDTO = await carsRepository.Create(mapper.Map<CarDTO>(carCreaterDTO));
				return new ServiceResult<CarDTO>(carDTO);
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
				var car = await carsRepository.Update(carDTO);
				return new ServiceResult<CarDTO>(car);
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
				var carDTO = await carsRepository.UpdateCount(id, count);
				return new ServiceResult<CarDTO>(carDTO);
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
				var carDTO = await carsRepository.MakeInaccessible(id);
				return new ServiceResult<CarDTO>(carDTO);
			}
			catch (Exception exception)
			{
				return new ServiceResult<CarDTO>(new BadRequestException(exception.Message));
			}
		}
	}
}
