using AutoMapper;
using CarsStorage.BLL.Abstractions.ModelsDTO.AuthModels;
using CarsStorageApi.Models.AuthModels;

namespace CarsStorageApi.Mappers
{
	/// <summary>
	/// Класс меппера при аутентификации пользователя.
	/// </summary>
	public class AuthMapperApi: Profile
	{
		public AuthMapperApi() 
		{
			CreateMap<RegisterUserDataRequest, UserRegisterDTO>();

			CreateMap<LoginDataRequest, UserLoginDTO>();

			CreateMap<JWTTokenDTO, JWTTokenRequestResponse>();
		}
	}
}
