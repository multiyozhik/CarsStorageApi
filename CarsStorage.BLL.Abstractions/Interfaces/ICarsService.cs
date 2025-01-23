using CarsStorage.BLL.Abstractions.Models;

namespace CarsStorage.BLL.Abstractions.Interfaces
{
    public interface ICarsService
    {
		public Task<ServiceResult<IEnumerable<CarDTO>>> GetList();

		public Task<ServiceResult<CarDTO>> Create(CarCreaterDTO carCreaterDTO);

		public Task<ServiceResult<CarDTO>> Update(CarDTO carDTO);

		public Task<ServiceResult<int>> Delete(int id);

		public Task<ServiceResult<CarDTO>> UpdateCount(int id, int count);
    }
}
