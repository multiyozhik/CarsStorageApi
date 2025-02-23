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
	public class CarsRepository(AppDbContext dbContext) : ICarsRepository
	{
		private readonly AppDbContext dbContext = dbContext;

		/// <summary>
		/// Метод получения всего списка автомобилей.
		/// </summary>
		public async Task<List<CarEntity>> GetList()
		{
			return await dbContext.Cars.ToListAsync();
		}			


		/// <summary>
		/// Метод создания новой записи автомобиля в БД.
		/// </summary>
		public async Task<CarEntity> Create(CarEntity carEntity)
		{
			await dbContext.Cars.AddAsync(carEntity);
			await dbContext.SaveChangesAsync();
			return carEntity;
		}


		/// <summary>
		/// Метод изменения записи данных автомобиля в БД.
		/// </summary>
		public async Task<CarEntity> Update(CarEntity carEntity)
		{
			var car = await dbContext.Cars.FirstOrDefaultAsync(c => c.Id == carEntity.Id)
				?? throw new Exception("Автомобиль с заданным Id не найден");

			car.Model = carEntity.Model;
			car.Make = carEntity.Make;
			car.Color = carEntity.Color;
			car.Count = carEntity.Count;
			car.IsAccassible = carEntity.IsAccassible;

			dbContext.Cars.Update(car);
			await dbContext.SaveChangesAsync();
			return car;	
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
		public async Task<CarEntity> UpdateCount(int id, int count)
		{
			var carEntity = await dbContext.Cars.FirstOrDefaultAsync(c => c.Id == id)
				?? throw new Exception("Автомобиль с заданным Id не найден");
			carEntity.Count = count;
			dbContext.Update(carEntity);
			await dbContext.SaveChangesAsync();
			return carEntity;
		}


		/// <summary>
		/// Метод для того, чтобы сделать запись об автомобиле с id недоступным для просмотра.
		/// </summary>
		public async Task<CarEntity> MakeInaccessible(int id)
		{
			var carEntity = await dbContext.Cars.FirstOrDefaultAsync(c => c.Id == id)
				?? throw new Exception("Автомобиль с заданным Id не найден");
			carEntity.IsAccassible = false;
			dbContext.Update(carEntity);
			await dbContext.SaveChangesAsync();
			return carEntity;
		}
	}
}
