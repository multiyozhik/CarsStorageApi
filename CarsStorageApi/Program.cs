using CarsStorage.BLL.Implementations;
using CarsStorage.DAL.EF;
using CarsStorage.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

Configure(app, app.Environment);


static void ConfigureServices(IServiceCollection services, IConfiguration config)
{
	services.AddControllers();
	services.AddEndpointsApiExplorer();
	services.AddSwaggerGen();

	services.AddScoped<CarsStorage.BLL.Interfaces.ICarsService, CarsService>();

	//services.AddDbContext<CarsAppDbContext>(options =>
	//	options.UseNpgsql(config.GetConnectionString("NpgConnection")));

	//services.AddIdentityCore<IdentityAppUser>();

	//services.AddDbContext<IdentityAppDbContext>(options =>
	//	options.UseNpgsql(config.GetConnectionString("NpgConnection")));

	//services.AddIdentity<IdentityAppUser, Microsoft.AspNetCore.Identity.IdentityRole>()
	//	.AddUserStore<IdentityAppDbContext>()
	//	.AddDefaultTokenProviders();   //AddEntityFrameworkStores<IdentityAppDbContext>() устарел, теперь AddUserStore<>()

	//services.Configure<IdentityOptions>(options =>
	//{
	//	options.User.RequireUniqueEmail = true;
	//	options.Password.RequiredLength = 5;
	//	options.Password.RequiredUniqueChars = 1;
	//	options.Password.RequireLowercase = false;
	//	options.Password.RequireDigit = false;
	//	options.Password.RequireNonAlphanumeric = false;
	//	options.Password.RequireUppercase = false;
	//	options.Lockout.MaxFailedAccessAttempts = 5;
	//	options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
	//});

	services.AddAuthentication();
}

static void Configure(WebApplication app, IHostEnvironment env)
{
	if (app.Environment.IsDevelopment())
	{
		app.UseSwagger();
		app.UseSwaggerUI();
	}

	app.UseHttpsRedirection();

	app.UseRouting();

	app.UseAuthentication();

	app.UseAuthorization();

	app.MapControllers();

	app.Run();
}
