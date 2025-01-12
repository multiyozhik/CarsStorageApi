using CarsStorage.BLL.Servises;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsStorage.BLL.Interfaces
{
	public interface ICarsService
	{
		public Task<IEnumerable<Car>> GetCarsList();
		public Task AddAsync(Car carDTO);
		public Task UpdateAsync(Car carDTO);
		public Task DeleteAsync(Guid id);
	}
}
