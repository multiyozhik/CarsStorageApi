using AutoMapper;
using CarsStorage.Abstractions.DAL.Repositories;
using CarsStorage.Abstractions.ModelsDTO.Car;
using CarsStorage.DAL.DbContexts;
using CarsStorage.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarsStorage.DAL.Repositories.Implementations
{
	/// <summary>
	/// Класс репозитория автомобилей.
	/// </summary>
	public class CarsRepository(AppDbContext dbContext, IMapper mapper) : ICarsRepository
	{
		private readonly AppDbContext dbContext = dbContext;

		/// <summary>
		/// Метод получения всего списка автомобилей.
		/// </summary>
		public async Task<List<CarDTO>> GetList()
		{
			var carEntityList = await dbContext.Cars.ToListAsync();
			return carEntityList.Select(mapper.Map<CarDTO>).ToList();
		}			


		/// <summary>
		/// Метод создания новой записи автомобиля в БД.
		/// </summary>
		public async Task<CarDTO> Create(CarDTO carDTO)
		{
			var carEntity = mapper.Map<CarEntity>(carDTO);
			await dbContext.Cars.AddAsync(carEntity);
			await dbContext.SaveChangesAsync();
			return mapper.Map<CarDTO>(carEntity);
		}


		/// <summary>
		/// Метод изменения записи данных автомобиля в БД.
		/// </summary>
		public async Task<CarDTO> Update(CarDTO carDTO)
		{
			var carEntity = await dbContext.Cars.FirstOrDefaultAsync(c => c.Id == carDTO.Id)
				?? throw new Exception("Автомобиль с заданным Id не найден");

			carEntity.Model = carDTO.Model;
			carEntity.Make = carDTO.Make;
			carEntity.Color = carDTO.Color;
			carEntity.IsAccassible = carDTO.IsAccassible;

			dbContext.Cars.Update(carEntity);
			await dbContext.SaveChangesAsync();
			return mapper.Map<CarDTO>(carEntity);
	
		}

		/// <summary>
		/// Метод удаления записи автомобиля по id.
		/// </summary>
		public async Task Delete(int id)
		{
			var carEntity = await dbContext.Cars.FirstOrDefaultAsync(c => c.Id == id)
				?? throw new Exception("Автомобиль с заданным Id не найден");
			dbContext.Cars.Remove(carEntity);
			await dbContext.SaveChangesAsync();
		}


		/// <summary>
		/// Метод для изменения количества автомобилей для id записи автомобиля в БД.
		/// </summary>
		public async Task<CarDTO> UpdateCount(int id, int count)
		{
			var carEntity = await dbContext.Cars.FirstOrDefaultAsync(c => c.Id == id)
				?? throw new Exception("Автомобиль с заданным Id не найден");
			carEntity.Count = count;
			dbContext.Update(carEntity);
			await dbContext.SaveChangesAsync();
			return mapper.Map<CarDTO>(carEntity);
		}


		/// <summary>
		/// Метод для того, чтобы сделать запись об автомобиле с id недоступным для просмотра.
		/// </summary>
		public async Task<CarDTO> MakeInaccessible(int id)
		{
			var carEntity = await dbContext.Cars.FirstOrDefaultAsync(c => c.Id == id)
				?? throw new Exception("Автомобиль с заданным Id не найден");
			carEntity.IsAccassible = false;
			dbContext.Update(carEntity);
			await dbContext.SaveChangesAsync();
			return mapper.Map<CarDTO>(carEntity);
		}
	}
}
