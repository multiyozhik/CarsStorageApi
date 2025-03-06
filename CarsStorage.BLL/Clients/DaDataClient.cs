using CarsStorage.Abstractions.ModelsDTO.Location;
using CarsStorage.BLL.Services.Config;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace CarsStorage.BLL.Services.Clients
{
	public class DaDataClient(IHttpClientFactory httpClientFactory, IOptions<DaDataApiConfig> options) : IDaDataClient
	{
		private readonly DaDataApiConfig daDataApiConfig = options.Value;

		public async Task<List<SuggestionDTO>> GetLocation(CoordinateDTO coordinateDTO)
		{
			var request = new HttpRequestMessage(HttpMethod.Get, new Uri(daDataApiConfig.LocationApiUrl));

			request.Headers.Authorization = new AuthenticationHeaderValue("Token", daDataApiConfig.Token);
			request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			var content = new StringContent(JsonSerializer.Serialize(coordinateDTO));
			content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			
			request.Content = content;

			var httpClient = httpClientFactory.CreateClient();
			var response = await httpClient.SendAsync(request);
			//var str = await response.Content.ReadAsStringAsync();
			var apiResponse = await response.Content.ReadFromJsonAsync<LocationApiResponse>();
			return apiResponse.suggestions;
		}
	}
}
