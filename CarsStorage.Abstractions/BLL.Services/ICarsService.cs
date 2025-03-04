using CarsStorage.Abstractions.General;
using CarsStorage.Abstractions.ModelsDTO.Car;

namespace CarsStorage.Abstractions.BLL.Services
{
	/// <summary>
	/// Интерфейс для сервиса автомобилей.
	/// </summary>
	public interface ICarsService
	{
		/// <summary>
		/// Метод возвращает список автомобилей.
		/// </summary>
		/// <returns>Список автомобилей.</returns>
		public Task<ServiceResult<List<CarDTO>>> GetList();


		/// <summary>
		/// Метод для создания объекта автомобиля.
		/// </summary>
		/// <param name="carCreaterDTO">Объект данных автомобиля.</param>
		/// <returns>Созданный объект автомобиля.</returns>
		public Task<ServiceResult<CarDTO>> Create(CarCreaterDTO carCreaterDTO);


		/// <summary>
		/// Метод для изменения объекта автомобиля.
		/// </summary>
		/// <param name="carDTO">Объект данных автомобиля.</param>
		/// <returns>Измененный объект автомобиля.</returns>
		public Task<ServiceResult<CarDTO>> Update(CarDTO carDTO);



		/// <summary>
		/// Метод для удаления объекта автомобиля.
		/// </summary>
		/// <param name="id">Идентификатор объекта автомобиля.</param>
		/// <returns>Идентификатор удаленного объекта автомобиля.</returns>
		public Task<ServiceResult<int>> Delete(int id);


		/// <summary>
		/// Метод для изменения количества объекта автомобиля.
		/// </summary>
		/// <param name="id">Идентификатор объекта автомобиля.</param>
		/// <param name="count">Количество автомобилей.</param>
		/// <returns>Измененный объект автомобиля.</returns>
		public Task<ServiceResult<CarDTO>> UpdateCount(int id, int count);


		/// <summary>
		/// Метод для указания, что объект автомобиля недоступен к просмотру.
		/// </summary>
		/// <param name="id">Идентификатор объекта автомобиля.</param>
		/// <returns>Измененный объект автомобиля.</returns>
		public Task<ServiceResult<CarDTO>> MakeInaccessible(int id);
	}
}
