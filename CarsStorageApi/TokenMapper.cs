using CarsStorage.BLL.Abstractions;
using CarsStorage.BLL.Config;
using Riok.Mapperly.Abstractions;

namespace CarsStorageApi
{
	[Mapper]
	public partial class TokenMapper
	{
		public partial TokenJWT TokenDtoToTokenJwt(TokenDTO tokenDTO);
		public partial TokenDTO TokenJwtToTokenDto(TokenJWT tokenJWT);
	}
}
