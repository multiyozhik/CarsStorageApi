using CarsStorage.BLL.Abstractions.Config;
using CarsStorage.BLL.Abstractions.Interfaces;
using CarsStorage.BLL.Abstractions.Mappers;
using CarsStorage.BLL.Implementations.Services;
using CarsStorage.BLL.Repositories.Implementations;
using CarsStorage.BLL.Repositories.Interfaces;
using CarsStorage.BLL.Repositories.Utils;
using CarsStorage.DAL.Config;
using CarsStorage.DAL.DbContexts;
using CarsStorageApi.Mappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

Configure(app, app.Environment);


static void ConfigureServices(IServiceCollection services, IConfiguration config)
{
	services.AddControllers();

	services.AddEndpointsApiExplorer();

	services.AddSwaggerGen();
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

	services.Configure<InitialConfig>(config.GetSection("InitialConfig"));
	services.Configure<JWTConfig>(config.GetSection("JwtConfig"));
	services.AddOptions<JWTConfig>();

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

	services.AddDbContext<AppDbContext>(options =>
		options.UseNpgsql(config.GetConnectionString("NpgConnection")));

	services.AddDbContext<AppDbContext>(options =>
		options.UseNpgsql(config.GetConnectionString("NpgConnection")));

	services
		.AddScoped<IRolesRepository, RolesRepository>()
		.AddScoped<IUsersRepository, UsersRepository>()
		.AddScoped<ICarsRepository, CarsRepository>()
		.AddScoped<IRolesService, RolesService>()
		.AddScoped<IUsersService, UsersService>()
		.AddScoped<ITokensService, TokensService>()
		.AddScoped<IPasswordHasher, PasswordHasher>()
		.AddScoped<IAuthenticateService, AuthenticateService>()
		.AddScoped<ICarsService, CarsService>();

	//подключение фильтра
	//services.AddControllers(options =>
	//{
	//	options.Filters.Add<GlobalExceptionFilter>();
	//});

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
			options.SaveToken = true;
			options.IncludeErrorDetails = true;
		})
		.AddBearerToken();

	services.AddAuthorizationBuilder()
		.AddPolicy("RequierManageUsers", policy => { policy.RequireClaim("CanManageUsers"); })
		.AddPolicy("RequierManageUsersRoles", policy => { policy.RequireClaim("CanManageUsersRoles"); })
		.AddPolicy("RequierManageCars", policy => { policy.RequireClaim("CanManageCars"); })
		.AddPolicy("RequierBrowseCars", policy => { policy.RequireClaim("CanBrowseCars"); });

	services.AddAutoMapper(typeof(AuthMapperApi), typeof(CarMapperApi), typeof(RoleMapperApi), typeof(UserMapperApi));
	services.AddAutoMapper(typeof(CarMapper), typeof(RoleMapper), typeof(UserMapper));
	//services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); //не подключилось автоматически
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

	//подключение middleware исключений
	//app.UseMiddleware<ExceptionHandlingMiddleware>();

	app.UseHsts();

	app.UseHttpsRedirection();

	app.UseRouting();

	app.UseStatusCodePages();

	app.UseAuthentication();

	app.UseAuthorization();

	app.MapControllers();

	app.Run();
}

