using AutoMapper;
using CarsStorage.Abstractions.ModelsDTO;
using CarsStorage.DAL.Models;

namespace CarsStorage.DAL.Repositories.Mappers
{
	public class RoleClaimMapper : Profile
	{
		public RoleClaimMapper() 
		{
			CreateMap<RoleClaimType, RoleClaimTypeBLL>();

			CreateMap<RoleClaimTypeBLL, RoleClaimType>();
		}
	}
}
