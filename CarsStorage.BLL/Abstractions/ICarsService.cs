using CarsStorage.BLL.Abstractions;
using CarsStorage.BLL.Servises;
using Microsoft.AspNetCore.Mvc;

namespace CarsStorage.BLL.Interfaces
{
	public interface ICarsService : ICrudService<Car>
	{
		public Task ChangeCount(Guid id, int count);
	}
}
