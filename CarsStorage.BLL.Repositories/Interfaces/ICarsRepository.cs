using CarsStorage.BLL.Abstractions.Models;
using CarsStorage.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsStorage.BLL.Repositories.Interfaces
{
	public interface ICarsRepository
	{
		public Task<IEnumerable<CarEntity>> GetList();
		public Task<CarEntity> Create(CarCreaterDTO carCreaterDTO);
		public Task<CarEntity> Update(CarDTO carDTO);
		public Task Delete(int id);
		public Task<CarEntity> UpdateCount(int id, int count);
	}
}
