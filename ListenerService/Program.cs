using ListenerService;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddOptions<RabbitMqConfig>().BindConfiguration("RabbitMqConfig");
builder.Services.AddHostedService<Listener>();

var host = builder.Build();

host.Run();


