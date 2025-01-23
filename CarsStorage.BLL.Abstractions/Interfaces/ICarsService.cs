using CarsStorage.BLL.Abstractions.Models;

namespace CarsStorage.BLL.Abstractions.Interfaces
{
    public interface ICarsService
    {
		public Task<IEnumerable<CarDTO>> GetList();
		public Task Create(CarCreaterDTO carCreaterDTO);
		public Task Update(CarDTO carDTO);
		public Task Delete(int id);
		public Task UpdateCount(int id, int count);
    }
}
