using CarsStorage.BLL.Abstractions.Models;
using CarsStorage.DAL.Entities;

namespace CarsStorage.BLL.Repositories.Interfaces
{
	public interface ICarsRepository
	{
		public Task<IEnumerable<CarEntity>> GetList();
		public Task<CarEntity> Create(CarCreaterDTO carCreaterDTO);
		public Task<CarEntity> Update(CarDTO carDTO);
		public Task Delete(int id);
		public Task<CarEntity> UpdateCount(int id, int count);
	}
}
