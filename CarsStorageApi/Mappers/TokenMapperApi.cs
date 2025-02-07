using AutoMapper;
using CarsStorage.BLL.Abstractions.ModelsDTO.AuthModels;
using CarsStorageApi.Models.AuthModels;

namespace CarsStorageApi.Mappers
{
	/// <summary>
	/// Класс меппера при аутентификации пользователя.
	/// </summary>
	public class TokenMapperApi: Profile
	{
		public TokenMapperApi() 
		{
			CreateMap<JWTTokenDTO, JWTTokenRequestResponse>();

			CreateMap<JWTTokenRequestResponse, JWTTokenDTO> ();
		}
	}
}
