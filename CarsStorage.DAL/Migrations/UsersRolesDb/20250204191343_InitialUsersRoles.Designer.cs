﻿// <auto-generated />
using System;
using CarsStorage.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CarsStorage.DAL.Migrations.UsersRolesDb
{
    [DbContext(typeof(UsersRolesDbContext))]
    [Migration("20250204191343_InitialUsersRoles")]
    partial class InitialUsersRoles
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CarsStorage.DAL.Entities.RoleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.PrimitiveCollection<int[]>("RoleClaims")
                        .IsRequired()
                        .HasColumnType("integer[]");

                    b.HasKey("Id");

                    b.ToTable("Roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "admin",
                            RoleClaims = new[] { 1, 2 }
                        },
                        new
                        {
                            Id = 2,
                            Name = "manager",
                            RoleClaims = new[] { 3, 0 }
                        },
                        new
                        {
                            Id = 3,
                            Name = "user",
                            RoleClaims = new[] { 0 }
                        });
                });

            modelBuilder.Entity("CarsStorage.DAL.Entities.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AccessToken")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<ValueTuple<string, string>>("PasswordHash")
                        .HasColumnType("record");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "admin@mail.ru",
                            PasswordHash = ("xVBr7uwC2CQk1fHbIuTevIMKhplishWMEbPZ4Ag5wXQ=", "kaTUv4wzcflJcX/Ed3zQiQ=="),
                            UserName = "admin"
                        });
                });

            modelBuilder.Entity("CarsStorage.DAL.Entities.UsersRolesEntity", b =>
                {
                    b.Property<int>("UserEntityId")
                        .HasColumnType("integer");

                    b.Property<int>("RoleEntityId")
                        .HasColumnType("integer");

                    b.HasKey("UserEntityId", "RoleEntityId");

                    b.HasIndex("RoleEntityId");

                    b.ToTable("UserRoles", (string)null);
                });

            modelBuilder.Entity("CarsStorage.DAL.Entities.UsersRolesEntity", b =>
                {
                    b.HasOne("CarsStorage.DAL.Entities.RoleEntity", "RoleEntity")
                        .WithMany("UserRolesList")
                        .HasForeignKey("RoleEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarsStorage.DAL.Entities.UserEntity", "UserEntity")
                        .WithMany("UserRolesList")
                        .HasForeignKey("UserEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RoleEntity");

                    b.Navigation("UserEntity");
                });

            modelBuilder.Entity("CarsStorage.DAL.Entities.RoleEntity", b =>
                {
                    b.Navigation("UserRolesList");
                });

            modelBuilder.Entity("CarsStorage.DAL.Entities.UserEntity", b =>
                {
                    b.Navigation("UserRolesList");
                });
#pragma warning restore 612, 618
        }
    }
}
