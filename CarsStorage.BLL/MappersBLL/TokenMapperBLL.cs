using AutoMapper;
using CarsStorage.Abstractions.ModelsDTO.Token;
using CarsStorage.DAL.Models;

namespace CarsStorage.BLL.Services.MappersBLL
{
	public class TokenMapperBLL : Profile
	{
		public TokenMapperBLL() 
		{ 
			CreateMap<JWTTokenDTO, JWTToken>();

			CreateMap<JWTToken, JWTTokenDTO>();
		}
	}
}
