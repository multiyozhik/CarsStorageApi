using CarsStorage.BLL.Abstractions;
using CarsStorage.BLL.Config;
using CarsStorage.BLL.Implementations;
using CarsStorage.BLL.Interfaces;
using CarsStorage.DAL.EF;
using CarsStorage.DAL.Entities;
using CarsStorageApi.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

Configure(app, app.Environment);


static void ConfigureServices(IServiceCollection services, IConfiguration config)
{
	services.AddControllers();

	services.AddEndpointsApiExplorer();
	services.AddSwaggerGen();

	//чтобы появилась кнопка Authorize в Swagger
	services.AddSwaggerGen(option =>
	{
		option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
		option.AddSecurityDefinition(
			"Bearer",
			new OpenApiSecurityScheme
			{
				In = ParameterLocation.Header,
				Description = "Please enter a valid token",
				Name = "Authorization",
				Type = SecuritySchemeType.Http,
				BearerFormat = "JWT",
				Scheme = "Bearer"
			}
		);
		option.AddSecurityRequirement(
			new OpenApiSecurityRequirement
			{
			{
				new OpenApiSecurityScheme
				{
					Reference = new OpenApiReference
					{
						Type = ReferenceType.SecurityScheme,
						Id = "Bearer"
					}
				},
				Array.Empty<string>()
			}
			}
		);
	});

	services.AddScoped<ICarsService, CarsService>();
	services.AddScoped<IAuthenticateService, AuthenticateService>();
	services.AddScoped<IUsersService, UsersService>();

	services.AddDbContext<CarsAppDbContext>(options =>
		options.UseNpgsql(config.GetConnectionString("NpgConnection")));

	services.AddDbContext<IdentityAppDbContext>(options =>
		options.UseNpgsql(config.GetConnectionString("NpgConnection")));

	services.AddDbContext<RolesDbContext>(options =>
		options.UseNpgsql(config.GetConnectionString("NpgConnection")));

	services.AddDbContext<UsersRolesDbContext>(options =>
		options.UseNpgsql(config.GetConnectionString("NpgConnection")));

	services.AddOptions<AdminConfig>();
	services.AddOptions<RoleNamesConfig>();
	services.AddOptions<JwtDTOConfig>();

	services.Configure<AdminConfig>(config.GetSection("AdminConfig"));
	services.Configure<RoleNamesConfig>(config.GetSection("RoleNamesConfig"));
	services.Configure<JwtDTOConfig>(config.GetSection("JWTConfig"));

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

	services.AddAuthentication(options =>
	{
		options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
	})
		.AddJwtBearer(options =>
		{
			var jwtConfig = config.GetSection("JWTConfig");
			if (!string.IsNullOrEmpty(jwtConfig["Key"]))
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(
						Convert.FromBase64String(jwtConfig["Key"])),
					ValidateIssuer = true,
					ValidIssuer = jwtConfig["Issuer"],
					ValidateAudience = true,
					ValidAudience = jwtConfig["Audience"],
					ValidateLifetime = true,
					RequireExpirationTime = true
				};
			}
			else
				throw new Exception("Не задан секретный ключ для конфигурации jwt авторизации");
		})
		.AddBearerToken();

	services.AddAuthorization();	
}

static void Configure(WebApplication app, IHostEnvironment env)
{
	if (app.Environment.IsDevelopment())
	{
		app.UseSwagger();
		app.UseSwaggerUI(c =>
		{
			c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
		});
	}

	app.UseHttpsRedirection();

	app.UseRouting();

	app.UseAuthentication();

	app.UseAuthorization();

	app.MapControllers();

	//CreateAdminAccount(app).Wait();

	//CreateRoles(app).Wait();

	app.Run();
}

static async Task CreateAdminAccount(IApplicationBuilder app)
{
	using IServiceScope scope = app.ApplicationServices.CreateScope();
	var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityAppUser>>();


	var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

	var admin = scope.ServiceProvider.GetRequiredService<IOptions<AdminConfig>>().Value;

	if (string.IsNullOrEmpty(admin.UserName)
		|| string.IsNullOrEmpty(admin.Password)
		|| string.IsNullOrEmpty(admin.Role))
		throw new Exception("Не заданы конфигурации для администратора");

	if (await userManager.FindByNameAsync(admin.UserName) is null)
	{
		await roleManager.CreateAsync(new IdentityRole(admin.Role));
		var adminUser = new IdentityAppUser()
		{
			UserName = admin.UserName,
			Email = admin.Email
		};

		if (await userManager.CreateAsync(adminUser, admin.Password) is not null)
		{
			await userManager.AddToRoleAsync(adminUser, admin.Role);
		}
	}
}

static async Task CreateRoles(IApplicationBuilder app)
{
	using IServiceScope scope = app.ApplicationServices.CreateScope();
	var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
	var defaultRoleNames = scope.ServiceProvider.GetRequiredService<IOptions<RoleNamesConfig>>().Value
		.DefaultRoleNamesList ?? throw new Exception("Не заданы конфигурации для начального списка ролей пользователей");

	foreach (var roleName in defaultRoleNames)
	{
		var roleExist = await roleManager.RoleExistsAsync(roleName);
		if (!roleExist)
			await roleManager.CreateAsync(new IdentityRole(roleName));
	}
}
