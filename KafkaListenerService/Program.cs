using KafkaListenerService.Kafka;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddConsumer<Message, MsgReceiveHandler>(builder.Configuration.GetSection("KafkaConfig"));

var host = builder.Build();
host.Run();
