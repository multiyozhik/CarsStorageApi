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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

Configure(app, app.Environment);

static void ConfigureServices(IServiceCollection services, IConfiguration config)
{
	(bool validateIssuer, bool validateAudience, bool validateLifetime, bool validateIssuerSigningKey, bool requireExpirationTime) = ValidateAppConfigs(config);

	services.AddOptions<InitialConfig>().BindConfiguration("InitialConfig");

	services.AddOptions<JWTConfig>().BindConfiguration("JWTConfig");

	services.AddControllers();

	services.AddEndpointsApiExplorer();	

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

	services.AddAuthentication(options =>
	{
		options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
	})
	.AddJwtBearer(options =>  {
		var jwt = config.GetSection("JWTConfig");
		options.TokenValidationParameters = new TokenValidationParameters
		{			
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(jwt["Key"])),
			ValidateIssuer = validateIssuer,
			ValidIssuer = jwt["Issuer"],
			ValidateAudience = validateAudience,
			ValidAudience = jwt["Audience"],
			ValidateLifetime = validateLifetime,
			RequireExpirationTime = requireExpirationTime
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

	services.AddAutoMapper(typeof(TokenMapperApi), typeof(CarMapperApi), typeof(RoleMapperApi), typeof(UserMapperApi));
	services.AddAutoMapper(typeof(CarMapper), typeof(RoleMapper), typeof(UserMapper));

	//services.AddAutoMapper(Assembly.Load("CarsStorageApi"));
	//services.AddAutoMapper(Assembly.Load("CarsStorage.BLL.Abstractions"));
	//services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); //не подключилось автоматически


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

static (bool validateIssuerResult, bool validateAudienceResult, bool validateLifetimeResult, bool validateIssuerSigningKeyResult, bool requireExpirationTimeResult) ValidateAppConfigs(IConfiguration jwtConfig)
{
	var initialConfig = jwtConfig.GetSection("InitialConfig")
		?? throw new Exception("Отсутствуют начальные конфигурации.");
	if (string.IsNullOrEmpty(initialConfig["DefaultRoleName"]))
		throw new Exception("Не определена роль пользователя при его регистрации в конфигурациях приложения.");

	var config = jwtConfig.GetSection("JWTConfig")
		?? throw new Exception("Отсутствуют конфигурации для токена.");
	if (string.IsNullOrEmpty(config["Key"]))
		throw new Exception("Не определен секретный ключ токена в конфигурации приложения.");
	if (string.IsNullOrEmpty(config["Issuer"]))
		throw new Exception("Не определен издатель токена в конфигурациях приложения.");
	if (string.IsNullOrEmpty(config["Audience"]))
		throw new Exception("Не определен получатель токена в конфигурациях приложения.");
	if (string.IsNullOrEmpty(config["ExpireMinutes"]))
		throw new Exception("Не определено время жизни токена в конфигурациях приложения.");

	var validateIssuer = config["ValidateIssuer"];
	var validateAudience = config["ValidateIssuer"];
	var validateLifetime = config["validateLifetime"];
	var validateIssuerSigningKey = config["ValidateIssuer"];
	var requireExpirationTime = config["ValidateIssuer"];

	var validateIssuerResult = true;
	var validateAudienceResult = true;
	var validateLifetimeResult = true;
	var validateIssuerSigningKeyResult = true;
	var requireExpirationTimeResult = true;

	if (!string.IsNullOrEmpty(validateIssuer) && !bool.TryParse(validateIssuer, out validateIssuerResult))
			throw new Exception("ValidateIssuer должен быть равным true или false.");
	if (!string.IsNullOrEmpty(validateAudience) && !bool.TryParse(validateAudience, out validateAudienceResult))		
		throw new Exception("ValidateAudience должен быть равным true или false.");
	if (!string.IsNullOrEmpty(validateLifetime) && !bool.TryParse(validateLifetime, out validateLifetimeResult))
		throw new Exception("ValidateLifetime должен быть равным true или false.");
	if (!string.IsNullOrEmpty(validateIssuerSigningKey) && !bool.TryParse(validateIssuerSigningKey, out validateIssuerSigningKeyResult))
		throw new Exception("ValidateIssuerSigningKey должен быть равным true или false.");
	if (!string.IsNullOrEmpty(requireExpirationTime) && !bool.TryParse(requireExpirationTime, out requireExpirationTimeResult))
		throw new Exception("RequireExpirationTime должен быть равным true или false.");

	return (validateIssuerResult, validateAudienceResult, validateLifetimeResult, validateIssuerSigningKeyResult, requireExpirationTimeResult);
}