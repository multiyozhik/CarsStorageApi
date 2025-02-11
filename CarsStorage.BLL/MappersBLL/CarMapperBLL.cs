using AutoMapper;
using CarsStorage.BLL.Abstractions.ModelsDTO.Car;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsStorage.BLL.Implementations.MappersBLL
{
	public class CarMapperBLL : Profile
	{
		public CarMapperBLL() 
		{
			CreateMap<CarCreaterDTO, CarDTO>();
		}
	}
}
