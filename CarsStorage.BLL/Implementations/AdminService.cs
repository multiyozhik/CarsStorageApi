using CarsStorage.BLL.Abstractions;

namespace CarsStorage.BLL.Implementations
{
	public class AdminService : IAdminService
	{

		public AdminService() 
		{ 
			
		
		}



		public Task<IEnumerable<AppUser>> GetList()
		{
			throw new NotImplementedException();
		}

		public Task AddAsync(AppUser appUser)
		{
			throw new NotImplementedException();
		}

		public Task DeleteAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync(AppUser appUser)
		{
			throw new NotImplementedException();
		}
	}
}
