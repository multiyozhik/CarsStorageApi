using CarsStorage.Abstractions.General;
using CarsStorage.Abstractions.ModelsDTO.Car;

namespace CarsStorage.Abstractions.BLL.Services
{
	/// <summary>
	/// Интерфейс для сервиса автомобилей.
	/// </summary>
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
