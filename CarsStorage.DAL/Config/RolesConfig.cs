using CarsStorage.DAL.Entities;
using CarsStorage.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarsStorage.DAL.Config
{
    public class RolesConfig : IEntityTypeConfiguration<RoleEntity>
    {
        private IEnumerable<RoleEntity> rolesList = [];
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            rolesList = rolesList.Append(new RoleEntity("admin") { Id = 1, RoleClaims = [RoleClaimType.CanManageUsers, RoleClaimType.CanManageRoles] });
            rolesList = rolesList.Append(new RoleEntity("manager") { Id = 2, RoleClaims = [RoleClaimType.CanManageCars, RoleClaimType.CanBrowseCars] });
            rolesList = rolesList.Append(new RoleEntity("user") { Id = 3, RoleClaims = [RoleClaimType.CanBrowseCars] });

            builder.HasData(rolesList);

            builder.HasKey(r => r.Id);
            builder.ToTable("Roles");
            builder.Property(r => r.Name).IsRequired();
            builder.Property(r => r.RoleClaims).IsRequired();
        }

        public IEnumerable<RoleEntity> GetRoles() => rolesList;
    }
}
