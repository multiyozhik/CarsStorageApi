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
        private readonly List<RoleEntity> rolesList = [
            new RoleEntity("admin") { Id = 1, RoleClaims = [RoleClaimType.CanManageUsers, RoleClaimType.CanManageRoles] },
            new RoleEntity("manager") { Id = 2, RoleClaims = [RoleClaimType.CanManageCars, RoleClaimType.CanBrowseCars] },
            new RoleEntity("user") { Id = 3, RoleClaims = [RoleClaimType.CanBrowseCars] }];

        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {            
            builder.HasData(rolesList);
            builder.HasKey(r => r.Id);
            builder.ToTable("Roles");
            builder.Property(r => r.Name).IsRequired();
            builder.Property(r => r.RoleClaims).IsRequired();
        }
    }
}
