using CarsStorage.BLL.Abstractions.Interfaces;
using CarsStorage.BLL.Abstractions.Models;
using CarsStorage.BLL.Implementations.Mappers;
using CarsStorage.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace CarsStorage.BLL.Implementations.Services
{
    public class CarsService(CarsAppDbContext dbContext) : ICarsService
	{
		private readonly CarMapper carMapper = new();

		private readonly CarsAppDbContext dbContext = dbContext;

		public async Task<IEnumerable<CarDTO>> GetList()
		{
			var carsList = await dbContext.Cars.ToListAsync();
			return carsList.Select(carMapper.CarEntityToCarDto);
		}

		public async Task Create(CarCreaterDTO carCreaterDTO)
		{

			await dbContext.Cars.AddAsync(carMapper.CarToCarEntity(carCreaterDTO));
			await dbContext.SaveChangesAsync();
		}
		public async Task Update(CarDTO car)
		{
			var i = await dbContext.Cars.Where(c => c.Id == car.Id)
				.ExecuteUpdateAsync(setters => setters
				.SetProperty(c => c.Model, car.Model)
				.SetProperty(c => c.Make, car.Make)
				.SetProperty(c => c.Color, car.Color)
				.SetProperty(c => c.Count, car.Count));
		}
		public async Task Delete(Guid id)
		{
			var i = await dbContext.Cars.Where(c => c.Id == id).ExecuteDeleteAsync();
		}

		public async Task UpdateCount(Guid id, int count)
		{
			await dbContext.Cars.Where(c => c.Id == id).ExecuteUpdateAsync(setters => setters.SetProperty(c => c.Count, count));
		}
	}
}
