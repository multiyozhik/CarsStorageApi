using CarsStorage.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Runtime.CompilerServices;

namespace CarsStorage.DAL.Models
{
	public class RolesConfig : IEntityTypeConfiguration<RoleEntity>
	{
		private IEnumerable<RoleEntity> roles;
		public void Configure(EntityTypeBuilder<RoleEntity> builder)
		{			
			var rolesList = new List<RoleEntity>
			{
				[0] = new RoleEntity("admin") { RoleClaims = [RoleClaimType.CanManageUsers, RoleClaimType.CanManageRoles] },
				[1] = new RoleEntity("manager") { RoleClaims = [RoleClaimType.CanManageUsers, RoleClaimType.CanManageRoles] },
				[2] = new RoleEntity("user") { RoleClaims = [RoleClaimType.CanManageUsers, RoleClaimType.CanManageRoles] }
			};

			builder.HasData(rolesList);
			builder.HasKey(r => r.Id);
			builder.ToTable("Roles");
			builder.Property(r => r.Name).IsRequired();
			builder.Property(r => r.RoleClaims).IsRequired();
		}

		public IEnumerable<RoleEntity> GetRoles() => roles;
	}
}
