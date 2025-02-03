using AutoMapper;
using CarsStorage.BLL.Abstractions.Models;
using CarsStorage.BLL.Repositories.Interfaces;
using CarsStorage.DAL.EF;
using CarsStorage.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarsStorage.BLL.Repositories.Implementations
{
	public class CarsRepository(CarsAppDbContext dbContext, IMapper mapper) : ICarsRepository
	{
		private readonly CarsAppDbContext dbContext = dbContext;

		public async Task<List<CarEntity>> GetList()
		{
			return await dbContext.Cars.ToListAsync();
		}


		public async Task<CarEntity> Create(CarCreaterDTO carCreaterDTO)
		{
			var carEntity = await dbContext.Cars.AddAsync(mapper.Map<CarEntity>(carCreaterDTO));
			await dbContext.SaveChangesAsync();
			return carEntity.Entity;
		}


		public async Task<CarEntity> Update(CarDTO carDTO)
		{
			var carEntity = await dbContext.Cars.FirstOrDefaultAsync(c => c.Id == carDTO.Id);
			if (carEntity is not null)
			{
				carEntity.Model = carDTO.Model;
				carEntity.Make = carDTO.Make;
				carEntity.Color = carDTO.Color;
				carEntity.Count = carDTO.Count;
				dbContext.Cars.Update(carEntity);
				await dbContext.SaveChangesAsync();
				return carEntity;
			}
			else
				throw new Exception("Автомобиль с заданным Id не найден");			
		}


		public async Task Delete(int id)
		{
			var carEntity = await dbContext.Cars.FirstOrDefaultAsync(c => c.Id == id);
			if (carEntity is not null)
			{
				dbContext.Cars.Remove(carEntity);
				await dbContext.SaveChangesAsync();
			}
			else
				throw new Exception("Автомобиль с заданным Id не найден");
		}


		public async Task<CarEntity> UpdateCount(int id, int count)
		{
			var carEntity = await dbContext.Cars.FirstOrDefaultAsync(c => c.Id == id);
			if (carEntity is not null)
			{
				carEntity.Count = count;
				await dbContext.SaveChangesAsync();
				return carEntity;
			}
			else
				throw new Exception("Автомобиль с заданным Id не найден");
		}

		public async Task<CarEntity> MakeInaccessible(int id)
		{
			var carEntity = await dbContext.Cars.FirstOrDefaultAsync(c => c.Id == id);
			if (carEntity is not null)
			{
				carEntity.IsAccassible = false;
				await dbContext.SaveChangesAsync();
				return carEntity;
			}
			else
				throw new Exception("Автомобиль с заданным Id не найден");
		}
	}
}
