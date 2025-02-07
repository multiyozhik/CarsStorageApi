using CarsStorage.BLL.Abstractions.ModelsDTO.CarDTO;
using CarsStorage.DAL.Entities;

namespace CarsStorage.BLL.Repositories.Interfaces
{
	/// <summary>
	/// Интерфейс для репозитория автомобилей.
	/// </summary>
    public interface ICarsRepository
	{
		public Task<List<CarEntity>> GetList();
		public Task<CarEntity> Create(CarEntity car);
		public Task<CarEntity> Update(CarEntity car);
		public Task Delete(int id);
		public Task<CarEntity> UpdateCount(int id, int count);
		public Task<CarEntity> MakeInaccessible(int id);
	}
}
