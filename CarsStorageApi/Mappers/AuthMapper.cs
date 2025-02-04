using AutoMapper;
using CarsStorage.BLL.Abstractions.ModelsDTO.AuthModels;
using CarsStorageApi.Models.AuthModels;

namespace CarsStorageApi.Mappers
{
	public class AuthMapper: Profile
	{
		public AuthMapper() 
		{
			CreateMap<RegisterUserDataRequest, UserRegisterDTO>();

			CreateMap<LoginDataRequest, UserLoginDTO>();

			CreateMap<JWTTokenDTO, JWTTokenRequestResponse>();
		}
	}
}
