using CarsStorage.DAL.Entities;

namespace CarsStorage.Abstractions.DAL.Repositories
{
	/// <summary>
	/// Интерфейс для репозитория автомобилей.
	/// </summary>
	public interface ICarsRepository
	{
		public Task<List<CarEntity>> GetList();
		public Task<CarEntity> Create(CarEntity carEntity);
		public Task<CarEntity> Update(CarEntity carEntity);
		public Task Delete(int id);
		public Task<CarEntity> UpdateCount(int id, int count);
		public Task<CarEntity> MakeInaccessible(int id);
	}
}
