using CarsStorage.BLL.Abstractions.Models;
using CarsStorage.BLL.Abstractions.ModelsDTO.CarDTO;

namespace CarsStorage.BLL.Abstractions.Interfaces
{
	public interface ICarsService
	{
		public Task<ServiceResult<List<CarDTO>>> GetList();

		public Task<ServiceResult<CarDTO>> Create(CarCreaterDTO carCreaterDTO);

		public Task<ServiceResult<CarDTO>> Update(CarDTO carDTO);

		public Task<ServiceResult<int>> Delete(int id);

		public Task<ServiceResult<CarDTO>> UpdateCount(int id, int count);

		public Task<ServiceResult<CarDTO>> MakeInaccessible(int id);
	}
}
