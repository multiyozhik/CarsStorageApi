using AutoMapper;
using CarsStorage.Abstractions.ModelsDTO;
using CarsStorage.DAL.Models;

namespace CarsStorage.BLL.Services.MappersBLL
{
	public class RoleClaimTypeMapper: Profile
	{
		public RoleClaimTypeMapper() 
		{
			CreateMap<RoleClaimTypeBLL, RoleClaimType>();

			CreateMap<RoleClaimType, RoleClaimTypeBLL>();
		}
	}
}
