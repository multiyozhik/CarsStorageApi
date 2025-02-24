using AutoMapper;
using CarsStorage.Abstractions.ModelsDTO.Token;
using CarsStorage.DAL.Models;

namespace CarsStorage.BLL.Services.MappersBLL
{
	/// <summary>
	/// Класс меппера для объектов токена.
	/// </summary>
	public class TokenMapperBLL : Profile
	{
		/// <summary>
		/// Конструктор меппера для объектов токена.
		/// </summary>
		public TokenMapperBLL() 
		{ 
			CreateMap<JWTTokenDTO, JWTToken>();

			CreateMap<JWTToken, JWTTokenDTO>();
		}
	}
}
