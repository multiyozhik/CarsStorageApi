using CarsStorage.Abstractions.BLL.Services;
using CarsStorage.Abstractions.Exceptions;
using CarsStorage.Abstractions.General;
using CarsStorage.Abstractions.ModelsDTO.Location;
using CarsStorage.BLL.Services.Clients;
using Microsoft.Extensions.Logging;

namespace CarsStorage.BLL.Services.Services
{
	/// <summary>
	/// Класс сервиса определения локации пользователя.
	/// </summary>
	/// <param name="daDataClient">Объект клиента сервиса DaData.</param>
	/// <param name="logger">Объект для логирования.</param>
	public class LocationService(IDaDataClient daDataClient, ILogger<LocationService> logger): ILocationService
	{
		/// <summary>
		/// Метод для определения локации пользователя по его координатам.
		/// </summary>
		/// <param name="coordinates">Объект координат пользователя.</param>
		/// <returns>Объект локации пользователя.</returns>
		public async Task<ServiceResult<LocationDTO>> GetUserLocation(CoordinateDTO coordinates)
		{
			try
			{
				var suggestionList = await daDataClient.GetLocation(coordinates);
				return new ServiceResult<LocationDTO>(
					new LocationDTO()
					{
						AddressList = suggestionList.Select(s => s.Unrestricted_value).ToList()
					});
			}
			catch (Exception exception)
			{
				logger.LogError("Ошибка в {service} в {method} при определении локации пользователя: {errorMessage}", this, nameof(this.GetUserLocation), exception.Message);
				throw new ServerException(exception.Message);
			}			
		}
	}
}
