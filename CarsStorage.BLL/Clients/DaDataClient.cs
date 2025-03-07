using CarsStorage.Abstractions.Exceptions;
using CarsStorage.Abstractions.ModelsDTO.Location;
using CarsStorage.BLL.Services.Config;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace CarsStorage.BLL.Services.Clients
{
	/// <summary>
	/// Класс клиента, взаимодействующего с DaData API.
	/// </summary>
	/// <param name="httpClientFactory">Объект фабрики для создания клиента для отправки запросов на API.</param>
	/// <param name="options">Объект конфигураций DaData API.</param>
	/// <param name="logger">Объект для логирования.</param>
	public class DaDataClient(IHttpClientFactory httpClientFactory, IOptions<DaDataApiConfig> options, ILogger<DaDataClient> logger) : IDaDataClient
	{
		private readonly DaDataApiConfig daDataApiConfig = options.Value;

		/// <summary>
		/// Метод для получения данных по локации пользователя.
		/// </summary>
		/// <param name="coordinateDTO">Объект координат пользователя.</param>
		/// <returns>Список возможных объектов с данными по локации пользователя.</returns>
		public async Task<List<SuggestionDTO>> GetLocation(CoordinateDTO coordinateDTO)
		{
			try
			{
				var request = new HttpRequestMessage(HttpMethod.Post, new Uri(daDataApiConfig.LocationApiUrl))
				{
					Headers =
				{
					Authorization = new AuthenticationHeaderValue("Token", daDataApiConfig.Token),
					Accept = { new MediaTypeWithQualityHeaderValue("application/json") }
				},
					Content = JsonContent.Create(coordinateDTO)
				};

				var httpClient = httpClientFactory.CreateClient();
				var response = await httpClient.SendAsync(request);
				response.EnsureSuccessStatusCode();

				var apiResponse = await response.Content.ReadFromJsonAsync<LocationApiResponse>();
				return apiResponse.Suggestions ?? [];
			}
			catch (Exception exception)
			{
				logger.LogError("Ошибка в {service} в {method}  при выполнении запроса к DaData API: {errorMessage}", this, nameof(this.GetLocation), exception.Message);
				throw new ServerException(exception.Message);
			}
		}
	}
}
