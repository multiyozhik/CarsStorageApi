using CarsStorage.DAL.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CarsStorage.DAL.EF
{
	public class IdentityAppDbContext(DbContextOptions<IdentityAppDbContext> options) : IdentityDbContext<IdentityAppUser>(options)
	{
	}
}
