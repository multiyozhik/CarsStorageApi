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
	/// <param name="carsRepository">Репозиторий автомобилей.</param>
	/// <param name="mapper">Объект меппера.</param>
	public class CarsService(ICarsRepository carsRepository, IMapper mapper) : ICarsService
	{
		/// <summary>
		/// Метод для получения списка всех автомобилей.
		/// </summary>
		/// <returns>Список всех автомобилей.</returns>
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
		/// Метод для создания нового объекта автомобиля.
		/// </summary>
		/// <param name="carCreaterDTO">Объект, представляющий данные для создания нового объекта автомобиля.</param>
		/// <returns>Созданный объект автомобиля.</returns>
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
		/// Метод для изменения данных автомобиля.
		/// </summary>
		/// <param name="carDTO">Объект для изменения данных автомобиля.</param>
		/// <returns>Измененный объект автомобиля.</returns>
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
		/// Метод для удаления объекта автомобиля по его идентификатору.
		/// </summary>
		/// <param name="id">Идентификатор автомобиля.</param>
		/// <returns>Идентификатор удаленного автомобиля.</returns>
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
		/// Метод для изменения количества автомобилей по его идентификатору.
		/// </summary>
		/// <param name="id">Идентификатор объекта автомобиля.</param>
		/// <param name="count">Новое значение количества автомобилей.</param>
		/// <returns>Измененный объект автомобиля.</returns>
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
		/// Метод для того, чтобы сделать объект автомобиля недоступным для просмотра по его идентификатору.
		/// </summary>
		/// <param name="id">Идентификатор автомобиля.</param>
		/// <returns>Измененный объект автомобиля.</returns>
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
