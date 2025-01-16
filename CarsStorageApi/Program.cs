using CarsStorage.BLL.Abstractions;
using CarsStorage.BLL.Implementations;
using CarsStorage.BLL.Interfaces;
using CarsStorage.DAL.EF;
using CarsStorage.DAL.Entities;
using CarsStorageApi.Config;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;


var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

Configure(app, app.Environment);


static void ConfigureServices(IServiceCollection services, IConfiguration config)
{
	services.AddControllers();

	services.AddEndpointsApiExplorer();
	services.AddSwaggerGen();

	services.AddScoped<ICarsService, CarsService>();
	services.AddScoped<IAuthenticateService, AuthenticateService>();
	services.AddScoped<IUsersService, UsersService>();

	services.AddDbContext<CarsAppDbContext>(options =>
		options.UseNpgsql(config.GetConnectionString("NpgConnection")));

	services.AddDbContext<IdentityAppDbContext>(options =>
		options.UseNpgsql(config.GetConnectionString("NpgConnection")));

	services.AddIdentity<IdentityAppUser, IdentityRole>()
		.AddEntityFrameworkStores<IdentityAppDbContext>()
		.AddDefaultTokenProviders(); 

	services.Configure<IdentityOptions>(options =>
	{
		options.User.RequireUniqueEmail = true;
		options.Password.RequiredLength = 5;
		options.Password.RequiredUniqueChars = 1;
		options.Password.RequireLowercase = false;
		options.Password.RequireDigit = false;
		options.Password.RequireNonAlphanumeric = false;
		options.Password.RequireUppercase = false;
		options.Lockout.MaxFailedAccessAttempts = 5;
		options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
	});

	services.AddAuthentication().AddBearerToken();  
	services.AddAuthorization();

	services.AddOptions<AdminConfig>();          //.ValidateDataAnnotations().ValidateOnStart();
	services.AddOptions<RoleNamesConfig>();

	services.Configure<AdminConfig>(config.GetSection("AdminConfig"));
	services.Configure<RoleNamesConfig>(config.GetSection("RoleNamesConfig"));
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

	CreateAdminAccount(app).Wait();

	CreateRoles(app).Wait();

	app.Run();
}

static async Task CreateAdminAccount(IApplicationBuilder app)
{
	using IServiceScope scope = app.ApplicationServices.CreateScope();
	var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityAppUser>>();
	var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

	var admin = scope.ServiceProvider.GetRequiredService<IOptions<AdminConfig>>().Value;
	var adminLogin = admin.UserName;
	var adminPassword = admin.Password;
	var adminEmail = admin.Email;
	var adminRole = admin.Role;
	
	if (await userManager.FindByNameAsync(adminLogin) is null)
	{
		await roleManager.CreateAsync(new IdentityRole(adminRole));
		var adminUser = new IdentityAppUser()
		{
			UserName = adminLogin,
			Email = adminEmail
		};

		if (await userManager.CreateAsync(adminUser, adminPassword) is not null)
		{
			await userManager.AddToRoleAsync(adminUser, adminRole);
		}
	}
}

static async Task CreateRoles(IApplicationBuilder app)
{
	using IServiceScope scope = app.ApplicationServices.CreateScope();
	var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
	var defaultRoleNames = scope.ServiceProvider.GetRequiredService<IOptions<RoleNamesConfig>>().Value.DefaultRoleNamesList;
	IdentityResult roleResult;

	foreach (var roleName in defaultRoleNames)
	{
		var roleExist = await roleManager.RoleExistsAsync(roleName);
		if (!roleExist)
		{
			roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
		}
	}
}
