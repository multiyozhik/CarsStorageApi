using CarsStorage.BLL.Abstractions;

namespace CarsStorage.BLL.Interfaces
{
	public interface ICarsService : ICrudService<Car>
	{
		public Task UpdateCount(Guid id, int count);
	}
}
