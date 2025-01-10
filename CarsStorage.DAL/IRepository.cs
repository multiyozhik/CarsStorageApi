namespace CarsStorage.DAL
{
	public interface IRepository<T> where T : class
	{
		Task<IEnumerable<T>> GetList();
		Task Add(T item);
		Task Update(T item);
		Task Delete(Guid id);
	}
}
