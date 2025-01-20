namespace CarsStorage.BLL.Abstractions
{
	public interface ICrudService<T> where T : class
	{
		public Task<IEnumerable<T>> GetList();
		public Task Create(T item);
		public Task Update(T item);
		public Task Delete(Guid id);
	}
}
