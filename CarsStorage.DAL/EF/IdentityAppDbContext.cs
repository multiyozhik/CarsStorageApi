using CarsStorage.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarsStorage.DAL.EF
{
	public class IdentityAppDbContext(DbContextOptions<IdentityAppDbContext> options) : IdentityDbContext<IdentityAppUser>(options)
	{
	}
}
