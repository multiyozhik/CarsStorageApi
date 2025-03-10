using CarsStorage.Abstractions.ModelsDTO.Location;

namespace CarsStorage.BLL.Services.Clients
{
	//Интерфейс для клиента, взаимодействующего с DaData API.
	public interface IDaDataClient
	{
		/// <summary>
		/// Метод для получения данных по локации пользователя.
		/// </summary>
		/// <param name="coordinateDTO">Объект координат пользователя.</param>
		/// <returns>Список возможных объектов с данными по локации пользователя.</returns>
		public Task<List<SuggestionDTO>> GetLocation(CoordinateDTO coordinateDTO);
	}
}
