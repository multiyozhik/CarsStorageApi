using CarsStorage.DAL.Entities;
using CarsStorage.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarsStorage.DAL.Config
{
	/// <summary>
	/// Класс определяет конфигурацию для RoleEntity сущности. 
	/// </summary>
	public class RolesConfig : IEntityTypeConfiguration<RoleEntity>
	{
		public static List<RoleEntity> AllRolesList { get; set; }
		public RolesConfig()
		{
			AllRolesList = [
				new RoleEntity("admin") { RoleEntityId = 1, RoleClaims = [RoleClaimType.CanManageUsers, RoleClaimType.CanManageRoles] },
				new RoleEntity("manager") { RoleEntityId = 2, RoleClaims = [RoleClaimType.CanManageCars, RoleClaimType.CanBrowseCars] },
				new RoleEntity("user") { RoleEntityId = 3, RoleClaims = [RoleClaimType.CanBrowseCars] }];
		}

		public void Configure(EntityTypeBuilder<RoleEntity> builder)
		{
			builder.HasData(AllRolesList);
			builder.HasKey(r => r.RoleEntityId);
			builder.ToTable("Roles");
			builder.Property(r => r.Name).IsRequired();
			builder.Property(r => r.RoleClaims).IsRequired();
		}
	}
}
