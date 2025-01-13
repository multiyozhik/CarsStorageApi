using CarsStorage.BLL.Implementations;
using CarsStorage.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.Identity.EntityFramework;
using CarsStorage.DAL.Entities;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<CarsStorage.BLL.Interfaces.ICarsService, CarsService>();

builder.Services.AddDbContext<CarsAppDbContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("NpgConnection")));


//builder.Services.AddDbContext<IdentityAppUsersDbContext>(options =>
//	options.UseNpgsql(builder.Configuration.GetConnectionString("UsersConnection")));

//builder.Services.AddIdentity<IdentityAppUser, IdentityRole>(opts =>
//{
//	opts.User.RequireUniqueEmail = true;
//	opts.Password.RequiredLength = 6;
//	opts.Password.RequireNonAlphanumeric = false;
//	opts.Password.RequireLowercase = false;
//	opts.Password.RequireUppercase = false;
//	opts.Password.RequireDigit = false;
//});

builder.Services.AddAuthentication();


var app = builder.Build();

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
