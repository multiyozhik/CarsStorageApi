using CarsStorage.Abstractions.DAL.Repositories;
using CarsStorage.DAL.DbContexts;
using CarsStorage.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarsStorage.DAL.Repositories.Implementations
{
	/// <summary>
	/// Класс репозитория автомобилей.
	/// </summary>
	/// <param name="dbContext">Объект контекста данных.</param>
	public class CarsRepository(AppDbContext dbContext) : ICarsRepository
	{
		private readonly AppDbContext dbContext = dbContext;

		/// <summary>
		/// Метод получения всего списка автомобилей.
		/// </summary>
		/// <returns>Список всех пользователей.</returns>
		public async Task<List<CarEntity>> GetList()
		{
			return await dbContext.Cars.ToListAsync();
		}


		/// <summary>
		/// Метод для создания объекта автомобиля.
		/// </summary>
		/// <param name="carEntity">Объект автомобиля.</param>
		/// <returns>Созданный объект автомобиля.</returns>
		public async Task<CarEntity> Create(CarEntity carEntity)
		{
			await dbContext.Cars.AddAsync(carEntity);
			await dbContext.SaveChangesAsync();
			return carEntity;
		}


		/// <summary>
		/// Метод для изменения объекта автомобиля.
		/// </summary>
		/// <param name="carEntity">Объект автомобиля.</param>
		/// <returns>Измененный объект автомобиля.</returns>
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
		/// Метод для удаления объекта автомобиля по его идентификатору.
		/// </summary>
		/// <param name="id">Идентификатор объекта автомобиля.</param>
		public async Task Delete(int id)
		{
			var carEntity = await dbContext.Cars.FirstOrDefaultAsync(c => c.Id == id)
				?? throw new Exception("Автомобиль с заданным Id не найден");
			dbContext.Cars.Remove(carEntity);
			await dbContext.SaveChangesAsync();
		}


		/// <summary>
		/// Метод для изменения количества объекта автомобиля.
		/// </summary>
		/// <param name="id">Идентификатор объекта автомобиля.</param>
		/// <param name="count">Новое значение количества автомобилей.</param>
		/// <returns>Измененный объект автомобиля.</returns>
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
		/// Метод для указания, что объект автомобиля недоступен к просмотру.
		/// </summary>
		/// <param name="id">Идентификатор объекта автомобиля.</param>
		/// <returns>Измененный объект автомобиля.</returns>
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
