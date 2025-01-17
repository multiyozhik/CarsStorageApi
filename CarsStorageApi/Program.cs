using CarsStorage.BLL.Abstractions;
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
				new string[] { }
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

	var jwtSection = config.GetSection("Jwt");
	var key = jwtSection.GetValue<string>("Key");
	var issuer = jwtSection.GetValue<string>("Issuer");
	var audience = jwtSection.GetValue<string>("Audience");
	var expireMinutes = jwtSection.GetValue<int>("ExpireMinutes");

	services.AddAuthentication(options =>
	{
		options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
	})
		.AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
				ValidateIssuer = true,
				ValidIssuer = issuer,
				ValidateAudience = true,
				ValidAudience = audience,
				ValidateLifetime = true,
				RequireExpirationTime = true
			};
		})
		.AddBearerToken();

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
