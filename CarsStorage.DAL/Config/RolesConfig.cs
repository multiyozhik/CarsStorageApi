using CarsStorage.DAL.Entities;
using CarsStorage.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarsStorage.DAL.Config
{
	/// <summary>
	/// Класс конфигурации для сущности роли пользователя. 
	/// </summary>
	public class RolesConfig : IEntityTypeConfiguration<RoleEntity>
	{
		/// <summary>
		/// Начальный список ролей в БД.
		/// </summary>
		private readonly List<RoleEntity> allRolesList = [
			new RoleEntity("admin") { RoleEntityId = 1, RoleClaims = [RoleClaimType.CanManageUsers, RoleClaimType.CanManageRoles] },
			new RoleEntity("manager") { RoleEntityId = 2, RoleClaims = [RoleClaimType.CanManageCars, RoleClaimType.CanBrowseCars] },
			new RoleEntity("user") { RoleEntityId = 3, RoleClaims = [RoleClaimType.CanBrowseCars] }];


		/// <summary>
		/// Метод конфигурирования сущности роли.
		/// </summary>
		/// <param name="builder">Объект API для конфигурирования.</param>
		public void Configure(EntityTypeBuilder<RoleEntity> builder)
		{
			builder.HasData(allRolesList);
			builder.HasKey(r => r.RoleEntityId);
			builder.Property(r => r.RoleEntityId).ValueGeneratedOnAdd();
			builder.HasIndex(u => u.Name).IsUnique();
			builder.ToTable("Roles");
			builder.Property(r => r.Name).IsRequired();
			builder.Property(r => r.RoleClaims).IsRequired();
		}
	}
}
