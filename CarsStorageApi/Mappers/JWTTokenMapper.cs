using CarsStorage.BLL.Abstractions.Models;
using CarsStorageApi.AuthModels;
using Riok.Mapperly.Abstractions;

namespace CarsStorageApi.Mappers
{
    [Mapper]
	public partial class JWTTokenMapper
	{
		public partial JWTTokenResponse JwtTokenDtoToJwtTokenResponse(JWTTokenDTO jwtTokenDTO);
	}
}
