﻿using AutoMapper;
using CarsStorage.BLL.Abstractions.ModelsDTO.CarDTO;
using CarsStorageApi.Models.CarModels;

namespace CarsStorageApi.Mappers
{
	/// <summary>
	/// Класс меппера для автомобилей.
	/// </summary>
	public class CarMapperApi : Profile
	{
		public CarMapperApi()
		{
			CreateMap<CarDTO, CarRequestResponse>();

			CreateMap<CarRequest, CarCreaterDTO>();

			CreateMap<CarRequestResponse, CarDTO>();
		}
	}
}
