using CarsStorage.BLL.AuthModels;
using CarsStorageApi.AuthModels;
using Riok.Mapperly.Abstractions;

namespace CarsStorageApi.Mappers
{
	[Mapper]
	public partial class TokenMapper
	{
		public partial TokenDTO TokenJwtToTokenDto(TokenJWT tokenJWT);
	}
}
