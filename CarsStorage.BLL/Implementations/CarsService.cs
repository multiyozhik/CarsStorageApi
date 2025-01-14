using CarsStorage.BLL.Interfaces;
using CarsStorage.BLL.Servises;
using CarsStorage.DAL;
using CarsStorage.DAL.EF;
using CarsStorage.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarsStorage.BLL.Implementations
{
	public class CarsService(CarsAppDbContext dbContext) : ICarsService
	{
		private readonly CarMapper carMapper = new CarMapper();

		private readonly CarsAppDbContext dbContext = dbContext;

		public async Task<IEnumerable<Car>> GetList()
		{
			var carsList = await dbContext.Cars.ToListAsync();
			return carsList.Select(carMapper.CarRowToCar);
		}

		public async Task AddAsync(Car car)
		{
			await dbContext.Cars.AddAsync(carMapper.CarToCarRow(car));
			await dbContext.SaveChangesAsync();
		}
		public async Task UpdateAsync(Car car)
		{
			var i = await dbContext.Cars.Where(c => c.Id == car.Id)
				.ExecuteUpdateAsync(setters => setters
				.SetProperty(c => c.Model, car.Model)
				.SetProperty(c => c.Make, car.Make)
				.SetProperty(c => c.Color, car.Color)
				.SetProperty(c => c.Count, car.Count));
		}
		public async Task DeleteAsync(Guid id)
		{
			var i = await dbContext.Cars.Where(c => c.Id == id).ExecuteDeleteAsync();
		}
	}
}
