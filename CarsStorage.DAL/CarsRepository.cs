using CarsStorage.DAL.EF;
using CarsStorage.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarsStorage.DAL
{
	public class CarsRepository(CarsAppDbContext dbContext) : IRepository<CarRow>
	{
		private readonly CarsAppDbContext dbContext = dbContext;

		public async Task<IEnumerable<CarRow>> GetList()
		{			
			return await dbContext.Cars.ToListAsync<CarRow>();
		}

		public async Task Add(CarRow carRow)
		{
			await dbContext.Cars.AddAsync(carRow);
		}
		
		public async Task Update(CarRow carRow)
		{
			var updatedCar = await dbContext.Cars.FirstOrDefaultAsync(car => car.Id == carRow.Id);
			updatedCar.Model = carRow.Model;
			updatedCar.Make = carRow.Make;
			updatedCar.Count = carRow.Count;
			await dbContext.SaveChangesAsync();
		}

		public async Task Delete(Guid id)
		{
			var deletedCar = await dbContext.Cars.FirstOrDefaultAsync(car => car.Id == id);
			dbContext.Cars.Remove(deletedCar);
			await dbContext.SaveChangesAsync();
		}
	}
}
