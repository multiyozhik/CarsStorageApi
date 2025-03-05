using AutoMapper;
using CarsStorage.Abstractions.ModelsDTO;
using CarsStorage.DAL.Models;

namespace CarsStorage.BLL.Services.MappersBLL
{
	/// <summary>
	/// Класс меппера для объектов утверждений ролей.
	/// </summary>
	public class RoleClaimTypeMapper: Profile
	{
		/// <summary>
		/// Конструктор меппера для объектов утверждений ролей.
		/// </summary>
		public RoleClaimTypeMapper() 
		{
			CreateMap<RoleClaimTypeBLL, RoleClaimType>();

			CreateMap<RoleClaimType, RoleClaimTypeBLL>();
		}
	}
}
