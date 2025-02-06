using AutoMapper;
using CarsStorage.BLL.Abstractions.ModelsDTO.CarDTO;
using CarsStorage.BLL.Repositories.Interfaces;
using CarsStorage.DAL.DbContexts;
using CarsStorage.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarsStorage.BLL.Repositories.Implementations
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
		public async Task<List<CarEntity>> GetList()
			=> await dbContext.Cars.ToListAsync();


		/// <summary>
		/// Метод создания новой записи автомобиля в БД.
		/// </summary>
		public async Task<CarEntity> Create(CarCreaterDTO carCreaterDTO)
		{
			var carEntity = await dbContext.Cars.AddAsync(mapper.Map<CarEntity>(carCreaterDTO));
			await dbContext.SaveChangesAsync();
			return carEntity.Entity;
		}


		/// <summary>
		/// Метод изменения записи данных автомобиля в БД.
		/// </summary>
		public async Task<CarEntity> Update(CarDTO carDTO)
		{
			var carEntity = await dbContext.Cars.FirstOrDefaultAsync(c => c.Id == carDTO.Id)
				?? throw new Exception("Автомобиль с заданным Id не найден");
			carEntity.Model = carDTO.Model;
			carEntity.Make = carDTO.Make;
			carEntity.Color = carDTO.Color;
			carEntity.Count = carDTO.Count;
			dbContext.Cars.Update(carEntity);
			await dbContext.SaveChangesAsync();
			return carEntity;
	
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
		/// Метод для изменения количества автомобилей.
		/// </summary>
		public async Task<CarEntity> UpdateCount(int id, int count)
		{
			var carEntity = await dbContext.Cars.FirstOrDefaultAsync(c => c.Id == id)
				?? throw new Exception("Автомобиль с заданным Id не найден");
			carEntity.Count = count;
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
			await dbContext.SaveChangesAsync();
			return carEntity;
		}
	}
}
