using AutoMapper;
using CarsStorage.BLL.Abstractions.ModelsDTO;
using CarsStorage.DAL.Models;

namespace CarsStorage.BLL.Repositories.Mappers
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
