using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsStorage.BLL.Interfaces
{
	public interface ICarsService
	{
		public Task<IEnumerable<CarDTO>> GetCarsList();
		public Task AddAsync(CarDTO carDTO);
		public Task UpdateAsync(CarDTO carDTO);
		public Task DeleteAsync(Guid id);
	}
}
