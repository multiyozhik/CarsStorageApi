using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace CarsStorage.DAL.Entities
{
	public class IdentityAppUser : IdentityUser
	{
		public DbSet<IEnumerable<RoleEntity>> Roles { get; set; }
	}
}
