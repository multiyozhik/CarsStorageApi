using AutoMapper;
using CarsStorage.BLL.Abstractions.ModelsDTO.Token;
using CarsStorageApi.Models.TokenModels;

namespace CarsStorageApi.MappersApi
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
