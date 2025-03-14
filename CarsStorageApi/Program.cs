﻿using CarsStorage.Abstractions.BLL.Services;
using CarsStorage.Abstractions.DAL.Repositories;
using CarsStorage.Abstractions.Exceptions;
using CarsStorage.Abstractions.ModelsDTO;
using CarsStorage.BLL.Services.Clients;
using CarsStorage.BLL.Services.Config;
using CarsStorage.BLL.Services.Services;
using CarsStorage.BLL.Services.Utils;
using CarsStorage.DAL.DbContexts;
using CarsStorage.DAL.Repositories.Implementations;
using CarsStorageApi.Config;
using CarsStorageApi.Filters;
using CarsStorageApi.Middlewares;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Octokit;
using Serilog;
using System.Reflection;
using System.Security.Claims;


var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostBuilderContext, logConfig) => logConfig.ReadFrom.Configuration(hostBuilderContext.Configuration));
//builder.WebHost.UseUrls(
//	"http://localhost:5243"
//	//"https://localhost:7251"
//);
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

Configure(app, app.Environment);



static void ConfigureServices(IServiceCollection services, IConfiguration config)
{
	ValidateAppConfigs(config);

	services.AddOptions<InitialConfig>().BindConfiguration("InitialConfig");

	services.AddOptions<JWTConfig>().BindConfiguration("JWTConfig");

	services.AddOptions<DaDataApiConfig>().BindConfiguration("DaDataApiConfig");

	services.AddDbContext<AppDbContext>(options =>
		options.UseNpgsql(config.GetConnectionString("NpgConnection")));

	services.AddHttpClient();

	services
		.AddScoped<IUsersRepository, UsersRepository>()
		.AddScoped<ICarsRepository, CarsRepository>()
		.AddScoped<ITokensRepository, TokensRepository>()
		.AddScoped<IUsersService, UsersService>()
		.AddScoped<ITokensService, TokensService>()
		.AddScoped<IPasswordHasher, PasswordHasher>()
		.AddScoped<IAuthenticateService, AuthenticateService>()
		.AddScoped<ICarsService, CarsService>()
		.AddScoped<ILocationService, LocationService>()
		.AddScoped<IDaDataClient, DaDataClient>()
		.AddScoped<ITechnicalWorksService, TechnicalWorksService>()
		.AddTransient<TechnicalWorksMiddleware>()
		.AddScoped<IDbStatesRepository, DbStatesRepository>()
		.AddScoped<AcceptHeaderActionFilter>();

	services.AddAuthentication(options =>
	{
		options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
		options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
	})
	.AddCookie()
	.AddJwtBearer(options =>
	{
		var jwt = config.GetSection("JWTConfig");
		options.TokenValidationParameters = new TokenValidationParameters
		{
			IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(jwt["Key"]
				?? throw new ServerException("Не определен секретный ключ токена в конфигурации приложения."))),
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
			?? throw new ServerException("Не определены конфигурации GitHub провайдера аутентификации");
		options.ClientId = gitHubConfig["ClientId"]
			?? throw new ServerException("Не определен идентификатор клиента");
		options.ClientSecret = gitHubConfig["ClientSecret"]
			?? throw new ServerException("Не определен секретный ключ клиента");
		options.CallbackPath = gitHubConfig["RedirectUri"]
			?? throw new ServerException("Не определен коллбек путь после GitHub аутентификации");
		options.Scope.Add(gitHubConfig["Scope"] ?? "");
		options.SaveTokens = true;

		options.AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
		options.TokenEndpoint = "https://github.com/login/oauth/access_token";
		options.UserInformationEndpoint = "https://api.github.com/user";

		options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
		options.ClaimActions.MapJsonKey(ClaimTypes.Name, "username");
		options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");

		options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

		options.Events = new OAuthEvents
		{
			OnCreatingTicket = async context =>
			{
				var token = context.AccessToken;
				var githubClient = new GitHubClient(new ProductHeaderValue("CarsStorageApi"))
				{
					Credentials = new Credentials(token)
				};
				var user = await githubClient.User.Current();
				context.Identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Login));
				context.Identity.AddClaim(new Claim(ClaimTypes.Name, user.Name ?? user.Login));
				context.Identity.AddClaim(new Claim(ClaimTypes.Email, user.Email ?? user.Login));
			}
		};
	})
	.AddGoogle(options =>
	{
		options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

		var googleConfig = config.GetSection("GoogleConfig")
			?? throw new ServerException("Не определены конфигурации Google провайдера аутентификации");
		options.ClientId = googleConfig["ClientId"]
			?? throw new ServerException("Не определен идентификатор клиента");
		options.ClientSecret = googleConfig["ClientSecret"]
			?? throw new ServerException("Не определен секретный ключ клиента");
		options.Scope.Add("email");
		options.Scope.Add("profile");
	});

	services.AddAuthorizationBuilder()
		.AddPolicy("RequierManageUsers", policy => { policy.RequireClaim(typeof(RoleClaimTypeBLL).ToString(), RoleClaimTypeBLL.CanManageUsers.ToString()); })
		.AddPolicy("RequierManageUsersRoles", policy => { policy.RequireClaim(typeof(RoleClaimTypeBLL).ToString(), RoleClaimTypeBLL.CanManageRoles.ToString()); })
		.AddPolicy("RequierManageCars", policy => { policy.RequireClaim(typeof(RoleClaimTypeBLL).ToString(), RoleClaimTypeBLL.CanManageCars.ToString()); })
		.AddPolicy("RequierBrowseCars", policy => { policy.RequireClaim(typeof(RoleClaimTypeBLL).ToString(), RoleClaimTypeBLL.CanBrowseCars.ToString()); });

	services.AddControllers();

	services.AddAutoMapper(Assembly.Load("CarsStorageApi"));
	services.AddAutoMapper(Assembly.Load("CarsStorage.BLL.Services"));
	services.AddAutoMapper(Assembly.Load("CarsStorage.DAL.Repositories"));

	services.AddSwaggerGen(option =>
	{
		option.SwaggerDoc("v1", new OpenApiInfo { Title = "CarsStorageAPI", Version = "v1", Description = "ASP.NET Core Web API для управления данными о хранящихся на складе автомобилях." });

		var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
		var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
		option.IncludeXmlComments(xmlPath);

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
			});
		option.AddSecurityRequirement(new OpenApiSecurityRequirement{
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
		});
	});

	services.AddCors(options => options.AddPolicy("CorsPolicy",
		builder => builder.AllowAnyOrigin().WithHeaders("Accept").AllowAnyMethod()));
}


static void Configure(WebApplication app, IHostEnvironment env)
{
	app.UseMiddleware<ExceptionHandlingMiddleware>();

	if (app.Environment.IsDevelopment())
	{
		app.UseSwagger();

		app.UseSwaggerUI(c =>
		{
			c.SwaggerEndpoint("/swagger/v1/swagger.json", "CarsStorage Api V1");
		});
	}

	app.UseHsts();

	app.UseHttpsRedirection();

	app.UseRouting();

	app.UseStatusCodePages();

	app.UseCors("CorsPolicy");

	app.UseAuthentication();

	app.UseAuthorization();

	app.UseRouting();

	app.UseMiddleware<TechnicalWorksMiddleware>();	

	app.UseWebSockets();

	app.Use(async (context, next) =>
	{
		if (context.WebSockets.IsWebSocketRequest)
		{
			var webSocket = await context.WebSockets.AcceptWebSocketAsync();
			var webSocketHandler = new WebSocketHandler(context.RequestServices.GetService<ITechnicalWorksService>());
			await webSocketHandler.HandleWebSocketConnection(webSocket);
		}
		else
		{
			await next();
		}
	});

	app.MapControllers();

	app.Run();
}

static void ValidateAppConfigs(IConfiguration jwtConfig)
{
	var initialConfig = jwtConfig.GetSection("InitialConfig")
	?? throw new ServerException("Отсутствуют начальные конфигурации.");
	if (string.IsNullOrEmpty(initialConfig["InitialRoleName"]))
		throw new ServerException("Не определена роль пользователя при его регистрации в конфигурациях приложения.");

	var config = jwtConfig.GetSection("JWTConfig")
		?? throw new ServerException("Отсутствуют конфигурации для токена.");
	if (string.IsNullOrEmpty(config["Key"]))
		throw new ServerException("Не определен секретный ключ токена в конфигурации приложения.");
	if (string.IsNullOrEmpty(config["Issuer"]))
		throw new ServerException("Не определен издатель токена в конфигурациях приложения.");
	if (string.IsNullOrEmpty(config["Audience"]))
		throw new ServerException("Не определен получатель токена в конфигурациях приложения.");
	if (string.IsNullOrEmpty(config["ExpireMinutes"]))
		throw new ServerException("Не определено время жизни токена в конфигурациях приложения.");
}

static bool GetParameterValue(string jwtParameter)
	=> (bool.TryParse(jwtParameter, out bool parameterValue))
	? parameterValue
	: throw new ServerException("Параметр валидации токена должен быть равным true или false.");
