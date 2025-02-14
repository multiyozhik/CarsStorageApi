using CarsStorage.Abstractions.BLL.Services;
using CarsStorage.Abstractions.DAL.Repositories;
using CarsStorage.Abstractions.ModelsDTO;
using CarsStorage.BLL.Services.Config;
using CarsStorage.BLL.Services.Services;
using CarsStorage.DAL.DbContexts;
using CarsStorage.DAL.Repositories.Implementations;
using CarsStorage.DAL.Repositories.Utils;
using CarsStorageApi.Filters;
using CarsStorageApi.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

Configure(app, app.Environment);

static void ConfigureServices(IServiceCollection services, IConfiguration config)
{
	ValidateAppConfigs(config);

	services.AddOptions<InitialConfig>().BindConfiguration("InitialConfig");

	services.AddOptions<JWTConfig>().BindConfiguration("JWTConfig");

	services.AddDbContext<AppDbContext>(options =>
		options.UseNpgsql(config.GetConnectionString("NpgConnection")));

	services
		.AddScoped<IUsersRepository, UsersRepository>()
		.AddScoped<ICarsRepository, CarsRepository>()
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
	.AddJwtBearer(options =>
	{
		var jwt = config.GetSection("JWTConfig");
		options.TokenValidationParameters = new TokenValidationParameters
		{
			IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(jwt["Key"]
				?? throw new Exception("�� ��������� ��������� ���� ������ � ������������ ����������."))),
			ValidIssuer = jwt["Issuer"],
			ValidAudience = jwt["Audience"],
			ValidateIssuer = GetParameterValue(jwt["ValidateIssuer"] ?? "true"),
			ValidateAudience = GetParameterValue(jwt["ValidateAudience"] ?? "true"),
			ValidateIssuerSigningKey = GetParameterValue(jwt["ValidateIssuerSigningKey"] ?? "true"),
			ValidateLifetime = GetParameterValue(jwt["ValidateLifetime"] ?? "true"),
			RequireExpirationTime = GetParameterValue(jwt["RequireExpirationTime"] ?? "true")
		};
		options.SaveToken = true;
		options.IncludeErrorDetails = true;
	})
	  .AddBearerToken()
	  .AddOAuth("GitHub", options =>
	  {
		  var gitHubConfig = config.GetSection("GitHubConfig")
			?? throw new Exception("�� ���������� ������������ Google ���������� ��������������");
		  options.ClientId = gitHubConfig["ClientId"]
			?? throw new Exception("�� ��������� ������������� �������");
		  options.ClientSecret = gitHubConfig["ClientSecret"]
			?? throw new Exception("�� ��������� ��������� ���� �������"); 
		  options.SaveTokens = true;
		  options.Scope.Add("userName"); 
		  options.Scope.Add("email"); 
		  options.CallbackPath = new PathString("/Authenticate/GitHubResponse");
	  });

	services.AddAuthorizationBuilder()
		.AddPolicy("RequierManageUsers", policy => { policy.RequireClaim(typeof(RoleClaimTypeBLL).ToString(), RoleClaimTypeBLL.CanManageUsers.ToString()); })
		.AddPolicy("RequierManageUsersRoles", policy => { policy.RequireClaim(typeof(RoleClaimTypeBLL).ToString(), RoleClaimTypeBLL.CanManageRoles.ToString()); })
		.AddPolicy("RequierManageCars", policy => { policy.RequireClaim(typeof(RoleClaimTypeBLL).ToString(), RoleClaimTypeBLL.CanManageCars.ToString()); })
		.AddPolicy("RequierBrowseCars", policy => { policy.RequireClaim(typeof(RoleClaimTypeBLL).ToString(), RoleClaimTypeBLL.CanBrowseCars.ToString()); });

	services.AddControllers();
	services.AddEndpointsApiExplorer();

	services.AddControllersWithViews(options =>
	{
		options.Filters.Add<GlobalExceptionFilter>();
	});

	services.AddAutoMapper(Assembly.Load("CarsStorageApi"));
	services.AddAutoMapper(Assembly.Load("CarsStorage.BLL.Implementations"));
	services.AddAutoMapper(Assembly.Load("CarsStorage.BLL.Repositories"));

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
	app.UseMiddleware<ExceptionHandlingMiddleware>();

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

	app.UseStatusCodePages();

	app.UseAuthentication();

	app.UseAuthorization();

	app.MapControllers();

	app.Run();
}

static void ValidateAppConfigs(IConfiguration jwtConfig)
{
	var initialConfig = jwtConfig.GetSection("InitialConfig")
		?? throw new Exception("����������� ��������� ������������.");
	if (string.IsNullOrEmpty(initialConfig["InitialRoleName"]))
		throw new Exception("�� ���������� ���� ������������ ��� ��� ����������� � ������������� ����������.");

	var config = jwtConfig.GetSection("JWTConfig")
		?? throw new Exception("����������� ������������ ��� ������.");
	if (string.IsNullOrEmpty(config["Key"]))
		throw new Exception("�� ��������� ��������� ���� ������ � ������������ ����������.");
	if (string.IsNullOrEmpty(config["Issuer"]))
		throw new Exception("�� ��������� �������� ������ � ������������� ����������.");
	if (string.IsNullOrEmpty(config["Audience"]))
		throw new Exception("�� ��������� ���������� ������ � ������������� ����������.");
	if (string.IsNullOrEmpty(config["ExpireMinutes"]))
		throw new Exception("�� ���������� ����� ����� ������ � ������������� ����������.");
}

static bool GetParameterValue(string jwtParameter)
	=> (bool.TryParse(jwtParameter, out bool parameterValue))
		? parameterValue
		: throw new Exception("�������� ��������� ������ ������ ���� ������ true ��� false.");


//ToDo: + ������������� ����� � Dal.Repositories, CarsStorage.Abstractions, � ������ Dal.Repositories � BLL.Services.
//ToDo: �������� tokenRepository  � � ��� ������������ dbContext.Users �� ����� �������� ���������� ������� � ��. � ��������.
//ToDo: ������� � BLL ����, ������� �� DAL � BLL ����, �.�. ��� �����, ���� �������� BLL ������ � �������� �� �� ������ ������? �������� ������ DAL-����, ������ ��� �� ��������� ������ BLL-������, � ��� ������� ���,
//� ��� ����, �������� EF �� ������ � DAL-������, �� BLL ��� ����� ����� DAL-������. �����������. ��� ����������� ������ �� ���� � ����� ������ EF-������, � � BLL - ������� � �������� � �� ��������.
//��������� ���������, � ��� ����� � ��������� ������.
//��������� ����������.
