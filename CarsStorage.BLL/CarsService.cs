using CarsStorage.BLL.Interfaces;
using CarsStorage.DAL;
using CarsStorage.DAL.EF;
using CarsStorage.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarsStorage.BLL
{
	public class CarsService(IRepository<CarRow> carsRepository) : ICarsService
	{
		private readonly IRepository<CarRow> carsRepository = carsRepository;
		private readonly CarMapper carMapper = new CarMapper();

		public async Task<IEnumerable<CarDTO>> GetCarsList()
		{
			var carsDTOList = await carsRepository.GetList();
			return carsDTOList.Select(carMapper.CarRowToCarDTO);
		}

		public async Task AddAsync(CarDTO carDTO)
		{
			await carsRepository.Add(carMapper.CarDTOToCarRow(carDTO));
		}
		public async Task UpdateAsync(CarDTO carDTO)
		{
			await carsRepository.Update(carMapper.CarDTOToCarRow(carDTO));
		}

		public async Task DeleteAsync(Guid id)
		{
			await carsRepository.Delete(id);
		}
	}
}
