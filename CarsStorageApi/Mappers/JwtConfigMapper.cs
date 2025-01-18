using CarsStorage.BLL.Config;
using CarsStorageApi.Config;
using Riok.Mapperly.Abstractions;

namespace CarsStorageApi.Mappers
{
	[Mapper]
	public partial class JwtConfigMapper
	{
		public partial JWTConfig JwtDTOConfigToJwt(JwtDTOConfig jwtDTOConfig);
	}
}
