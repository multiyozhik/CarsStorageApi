using AutoMapper;
using CarsStorage.Abstractions.ModelsDTO.Token;
using CarsStorageApi.Models.TokenModels;

namespace CarsStorageApi.MappersApi
{
	/// <summary>
	/// Класс меппера для объектов токенов.
	/// </summary>
	public class TokenMapperApi: Profile
	{
		/// <summary>
		/// Конструктор меппера для объектов токенов.
		/// </summary>
		public TokenMapperApi() 
		{
			CreateMap<JWTTokenDTO, JWTTokenRequestResponse>();

			CreateMap<JWTTokenRequestResponse, JWTTokenDTO> ();
		}
	}
}
