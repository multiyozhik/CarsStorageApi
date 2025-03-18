using RabbitListener;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IListener, Listener>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

//app.Map("/Receive/{message}", async (IListener listener, string message) => await listener.Recieve(message));

app.Run();
