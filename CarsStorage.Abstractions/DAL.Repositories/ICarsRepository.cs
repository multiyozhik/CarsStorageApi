using CarsStorage.DAL.Entities;

namespace CarsStorage.Abstractions.DAL.Repositories
{
	/// <summary>
	/// Интерфейс для репозитория автомобилей.
	/// </summary>
	public interface ICarsRepository
	{
		/// <summary>
		/// Метод для получения списка автомобилей.
		/// </summary>
		/// <returns>Список автомобилей.</returns>
		public Task<List<CarEntity>> GetList();


		/// <summary>
		/// Метод для создания объекта автомобиля.
		/// </summary>
		/// <param name="carEntity">Объект автомобиля.</param>
		/// <returns>Созданный объект автомобиля.</returns>
		public Task<CarEntity> Create(CarEntity carEntity);


		/// <summary>
		/// Метод для изменения объекта автомобиля.
		/// </summary>
		/// <param name="carEntity">Объект автомобиля.</param>
		/// <returns>Измененный объект автомобиля.</returns>
		public Task<CarEntity> Update(CarEntity carEntity);


		/// <summary>
		/// Метод для удаления объекта автомобиля по его идентификатору.
		/// </summary>
		/// <param name="id">Идентификатор объекта автомобиля.</param>
		public Task Delete(int id);


		/// <summary>
		/// Метод для изменения количества объекта автомобиля.
		/// </summary>
		/// <param name="id">Идентификатор объекта автомобиля.</param>
		/// <param name="count">Новое значение количества автомобилей.</param>
		/// <returns>Измененный объект автомобиля.</returns>
		public Task<CarEntity> UpdateCount(int id, int count);


		/// <summary>
		/// Метод для указания, что объект автомобиля недоступен к просмотру.
		/// </summary>
		/// <param name="id">Идентификатор объекта автомобиля.</param>
		/// <returns>Измененный объект автомобиля.</returns>
		public Task<CarEntity> MakeInaccessible(int id);
	}
}
