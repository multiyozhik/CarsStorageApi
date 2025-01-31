using CarsStorage.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarsStorage.DAL.Models
{
	public class RolesConfig : IEntityTypeConfiguration<RoleEntity>
	{
		private IEnumerable<RoleEntity> rolesList = [];
		public void Configure(EntityTypeBuilder<RoleEntity> builder)
		{
			rolesList = rolesList.Append(new RoleEntity("admin") { RoleClaims = [RoleClaimType.CanManageUsers, RoleClaimType.CanManageRoles] });
			rolesList = rolesList.Append(new RoleEntity("manager") { RoleClaims = [RoleClaimType.CanManageCars, RoleClaimType.CanBrowseCars] });
			rolesList = rolesList.Append(new RoleEntity("user") { RoleClaims = [RoleClaimType.CanBrowseCars] });

			builder.HasData(rolesList);

			builder.HasKey(r => r.Id);
			builder.ToTable("Roles");
			builder.Property(r => r.Name).IsRequired();
			builder.Property(r => r.RoleClaims).IsRequired();
		}

		public IEnumerable<RoleEntity> GetRoles() => rolesList;
	}
}
