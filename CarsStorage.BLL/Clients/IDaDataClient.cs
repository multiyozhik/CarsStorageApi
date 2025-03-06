using CarsStorage.Abstractions.ModelsDTO.Location;

namespace CarsStorage.BLL.Services.Clients
{
	public interface IDaDataClient
	{
		public Task<List<SuggestionDTO>> GetLocation(CoordinateDTO coordinateDTO);
	}
}
