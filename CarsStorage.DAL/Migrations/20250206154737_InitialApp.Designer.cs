﻿// <auto-generated />
using CarsStorage.DAL.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CarsStorage.DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250206154737_InitialApp")]
    partial class InitialApp
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

            modelBuilder.Entity("CarsStorage.DAL.Entities.RoleEntity", b =>
                {
                    b.Property<int>("RoleEntityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RoleEntityId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.PrimitiveCollection<int[]>("RoleClaims")
                        .IsRequired()
                        .HasColumnType("integer[]");

                    b.HasKey("RoleEntityId");

                    b.ToTable("Roles", (string)null);

                    b.HasData(
                        new
                        {
                            RoleEntityId = 1,
                            Name = "admin",
                            RoleClaims = new[] { 2, 3 }
                        },
                        new
                        {
                            RoleEntityId = 2,
                            Name = "manager",
                            RoleClaims = new[] { 4, 1 }
                        },
                        new
                        {
                            RoleEntityId = 3,
                            Name = "user",
                            RoleClaims = new[] { 1 }
                        });
                });

            modelBuilder.Entity("CarsStorage.DAL.Entities.UserEntity", b =>
                {
                    b.Property<int>("UserEntityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserEntityId"));

                    b.Property<string>("AccessToken")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Hash")
                        .HasColumnType("text");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<string>("Salt")
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserEntityId");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            UserEntityId = 1,
                            Email = "admin@mail.ru",
                            Hash = "JRxt+pwxtgCpgGS7TLXEjsPxV1ll/IZhLZSvN6QDzdc=",
                            Salt = "+9jY4swyva9aJKzTI/5mHQ==",
                            UserName = "admin"
                        },
                        new
                        {
                            UserEntityId = 2,
                            Email = "manager@mail.ru",
                            Hash = "adA8MaXGA/uwLaAtQ+ikggavMypELr5NK+V+KyB4l4U=",
                            Salt = "Dxid1tYUAovqRAvao2lBtQ==",
                            UserName = "manager"
                        },
                        new
                        {
                            UserEntityId = 3,
                            Email = "user3@mail.ru",
                            Hash = "Y0M0qRE2drIzyrcznkG1DsEhkrCDmI1GzkzXEpsv9yU=",
                            Salt = "LolXd97/5wfAEqROD/8bMg==",
                            UserName = "user3"
                        },
                        new
                        {
                            UserEntityId = 4,
                            Email = "user4@mail.ru",
                            Hash = "DQaXnrG7s5V9HUHoQiWAwhMpe661ZJlJdQ661J+n8gQ=",
                            Salt = "BgqW+f6XxuldT0UltY6vMg==",
                            UserName = "user4"
                        },
                        new
                        {
                            UserEntityId = 5,
                            Email = "user5@mail.ru",
                            Hash = "eZw/yjr7gGpoaLUABw8Fgz2rNIMwLnqAN6o0S8tLS+8=",
                            Salt = "tQwRgbrUoOK519vJt2X10Q==",
                            UserName = "user5"
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

                    b.ToTable("UsersRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserEntityId = 1,
                            RoleEntityId = 1
                        },
                        new
                        {
                            UserEntityId = 2,
                            RoleEntityId = 2
                        },
                        new
                        {
                            UserEntityId = 3,
                            RoleEntityId = 2
                        },
                        new
                        {
                            UserEntityId = 4,
                            RoleEntityId = 3
                        },
                        new
                        {
                            UserEntityId = 5,
                            RoleEntityId = 3
                        });
                });

            modelBuilder.Entity("RoleEntityUserEntity", b =>
                {
                    b.Property<int>("RolesListRoleEntityId")
                        .HasColumnType("integer");

                    b.Property<int>("UsersListUserEntityId")
                        .HasColumnType("integer");

                    b.HasKey("RolesListRoleEntityId", "UsersListUserEntityId");

                    b.HasIndex("UsersListUserEntityId");

                    b.ToTable("RoleEntityUserEntity");
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

            modelBuilder.Entity("RoleEntityUserEntity", b =>
                {
                    b.HasOne("CarsStorage.DAL.Entities.RoleEntity", null)
                        .WithMany()
                        .HasForeignKey("RolesListRoleEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarsStorage.DAL.Entities.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UsersListUserEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
