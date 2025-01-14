namespace CarsStorage.BLL.Abstractions
{
	public interface ICrudService<T> where T : class
	{
		public Task<IEnumerable<T>> GetList();
		public Task AddAsync(T item);
		public Task UpdateAsync(T item);
		public Task DeleteAsync(Guid id);
	}
}
