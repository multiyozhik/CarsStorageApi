using CarsStorage.BLL.Abstractions.Interfaces;
using CarsStorage.BLL.Abstractions.Models;
using CarsStorage.BLL.Implementations.Services;
using CarsStorage.BLL.Repositories.Implementations;
using CarsStorage.BLL.Repositories.Interfaces;
using CarsStorage.DAL.EF;
using CarsStorage.DAL.Entities;
using CarsStorage.DAL.Models;
using CarsStorageApi.Config;
using CarsStorageApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;


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

	services.
		AddScoped<ICarsService, CarsService>()
		.AddScoped<IAuthenticateService, AuthenticateService>();
	services.AddScoped<IUsersService, UsersService>();
	services.AddScoped<IRolesService, RolesService>();
	services.AddScoped<ITokenService, TokenService>();
	services.AddScoped<ICarsRepository, CarsRepository>();
	services.AddScoped<IUsersRepository, UsersRepository>();

	services.AddDbContext<CarsAppDbContext>(options =>
		options.UseNpgsql(config.GetConnectionString("NpgConnection")));

	services.AddDbContext<IdentityAppDbContext>(options =>
		options.UseNpgsql(config.GetConnectionString("NpgConnection")));

	services.AddOptions<AdminConfig>();
	services.AddOptions<JWTConfigDTO>();

	services.Configure<AdminConfig>(config.GetSection("AdminConfig"));
	services.Configure<JWTConfigDTO>(config.GetSection("JwtConfig"));
	services.Configure<InitialDbSeedConfig>(config.GetSection("InitialDbSeedConfig"));

	services.AddIdentity<IdentityAppUser, IdentityRole>()
		.AddEntityFrameworkStores<IdentityAppDbContext>()
		.AddDefaultTokenProviders();

	services.AddTransient<PasswordHasher<IdentityAppUser>>();

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
			var jwtConfig = config.GetSection("JWTConfig") ?? throw new Exception("Не заданы конфигурации валидации для jwt-токена авторизации");
			if (string.IsNullOrEmpty(jwtConfig["Key"]))
				throw new Exception("Не задан секретный ключ jwt-токена авторизации");
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(jwtConfig["Key"])),
				ValidateIssuer = bool.Parse(jwtConfig["ValidateIssuer"] ?? "true"),
				ValidIssuer = jwtConfig["Issuer"],
				ValidateAudience = bool.Parse(jwtConfig["ValidateAudience"] ?? "true"),
				ValidAudience = jwtConfig["Audience"],
				ValidateLifetime = bool.Parse(jwtConfig["ValidateLifetime"] ?? "true"),
				RequireExpirationTime = bool.Parse(jwtConfig["RequireExpirationTime"] ?? "true")
			};
		})
		.AddBearerToken();

	services.AddAuthorizationBuilder()
		.AddPolicy("RequierManageUsers", policy => { policy.RequireClaim("CanManageUsers"); })
		.AddPolicy("RequierManageUsersRoles", policy => { policy.RequireClaim("CanManageUsersRoles"); })
		.AddPolicy("RequierManageCars", policy => { policy.RequireClaim("CanManageCars"); })
		.AddPolicy("RequierBrowseCars", policy => { policy.RequireClaim("CanBrowseCars"); });

	services.AddAutoMapper(typeof(MappingProfileApi));
	services.AddAutoMapper(typeof(MappingProfileDTO));
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

	app.UseHsts();

	app.UseHttpsRedirection();

	app.UseRouting();

	app.UseAuthentication();

	app.UseAuthorization();

	app.MapControllers();

	CreateAdminAccount(app.Services).Wait();  

	app.Run();
}

static async Task CreateAdminAccount(IServiceProvider serviceProvider)
{
	using IServiceScope scope = serviceProvider.CreateScope();
	var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityAppUser>>();
	var passwordHasher = scope.ServiceProvider.GetRequiredService<PasswordHasher<IdentityAppUser>>();
	var admin = scope.ServiceProvider.GetRequiredService<IOptions<AdminConfig>>().Value;

	if (string.IsNullOrEmpty(admin.UserName)
		|| string.IsNullOrEmpty(admin.Password)
		|| string.IsNullOrEmpty(admin.Role))
		throw new Exception("Не заданы конфигурации для администратора");

	if (await userManager.FindByNameAsync(admin.UserName) is null)
	{
		var adminUser = new IdentityAppUser()
		{
			Id = "0",
			UserName = admin.UserName,
			Email = admin.Email
		};

		var result = await userManager.CreateAsync(adminUser, passwordHasher.HashPassword(adminUser, admin.Password));
		if (result.Succeeded)
			adminUser.RolesList = [new RoleEntity(admin.UserName)];

		//ToDo: System.AggregateException: "One or more errors occurred. (The entity type 'IdentityUserLogin<string>'
		//requires a primary key to be defined. If you intended to use a keyless entity type, call 'HasNoKey' in 'OnModelCreating'.
		//For more information on keyless entity types, see https://go.microsoft.com/fwlink/?linkid=2141943.)"


		//await appDbContext.UserRoles.AddAsync(new UsersRolesEntity()
		//{
		//	IdentityAppUser = adminUser,
		//	IdentityAppUserId = int.Parse(adminUser.Id),
		//	RoleEntity = adminrole,
		//	RoleEntityId = adminrole.Id
		//});
		//await appDbContext.SaveChangesAsync();
	}
}