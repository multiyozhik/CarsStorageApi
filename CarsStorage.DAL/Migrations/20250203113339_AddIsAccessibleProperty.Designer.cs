﻿// <auto-generated />
using CarsStorage.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CarsStorage.DAL.Migrations
{
    [DbContext(typeof(CarsDbContext))]
    [Migration("20250203113339_AddIsAccessibleProperty")]
    partial class AddIsAccessibleProperty
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CarsStorage.DAL.Entities.CarEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.Property<bool>("IsAccassible")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<string>("Make")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Cars", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Color = "красный",
                            Count = 4,
                            IsAccassible = true,
                            Make = "Kalina",
                            Model = "Lada"
                        },
                        new
                        {
                            Id = 2,
                            Color = "белый",
                            Count = 8,
                            IsAccassible = true,
                            Make = "J7",
                            Model = "JAC"
                        },
                        new
                        {
                            Id = 3,
                            Color = "синий",
                            Count = 6,
                            IsAccassible = true,
                            Make = "Granta",
                            Model = "Lada"
                        },
                        new
                        {
                            Id = 4,
                            Color = "черный",
                            Count = 5,
                            IsAccassible = true,
                            Make = "G8",
                            Model = "Audi"
                        },
                        new
                        {
                            Id = 5,
                            Color = "серый",
                            Count = 2,
                            IsAccassible = true,
                            Make = "Tigo 4",
                            Model = "Cherry"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
