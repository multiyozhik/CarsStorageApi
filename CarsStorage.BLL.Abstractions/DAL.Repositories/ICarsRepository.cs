using CarsStorage.Abstractions.ModelsDTO.Car;

namespace CarsStorage.Abstractions.DAL.Repositories
{
	/// <summary>
	/// Интерфейс для репозитория автомобилей.
	/// </summary>
	public interface ICarsRepository
	{
		public Task<List<CarDTO>> GetList();
		public Task<CarDTO> Create(CarDTO car);
		public Task<CarDTO> Update(CarDTO car);
		public Task Delete(int id);
		public Task<CarDTO> UpdateCount(int id, int count);
		public Task<CarDTO> MakeInaccessible(int id);
	}
}
