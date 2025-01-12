using CarsStorage.BLL.Implementations;
using CarsStorage.DAL.EF;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<CarsStorage.BLL.Interfaces.ICarsService, CarsService>();
builder.Services.AddDbContext<CarsAppDbContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("NpgConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
