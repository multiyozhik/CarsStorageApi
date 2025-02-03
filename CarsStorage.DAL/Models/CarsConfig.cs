﻿using CarsStorage.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarsStorage.DAL.Models
{
	public class CarsConfig : IEntityTypeConfiguration<CarEntity>
	{
		public void Configure(EntityTypeBuilder<CarEntity> builder)
		{
			builder.HasData(
					new CarEntity { Id = 1, Model = "Lada", Make = "Kalina", Color = "красный", Count = 4 },
					new CarEntity { Id = 2, Model = "JAC", Make = "J7", Color = "белый", Count = 8 },
					new CarEntity { Id = 3, Model = "Lada", Make = "Granta", Color = "синий", Count = 6 },
					new CarEntity { Id = 4, Model = "Audi", Make = "G8", Color = "черный", Count = 5 },
					new CarEntity { Id = 5, Model = "Cherry", Make = "Tigo 4", Color = "серый", Count = 2 });

			builder.HasKey(c => c.Id);
			builder.ToTable("Cars");
			builder.Property(c => c.Model).IsRequired();
			builder.Property(c => c.Make).IsRequired();
			builder.Property(c => c.Color).IsRequired();
			builder.Property(c => c.Count).IsRequired();
		}
	}
}
