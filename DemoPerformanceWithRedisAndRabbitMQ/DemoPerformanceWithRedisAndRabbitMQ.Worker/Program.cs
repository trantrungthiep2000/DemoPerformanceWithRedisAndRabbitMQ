using DemoPerformanceWithRedisAndRabbitMQ.Worker.Extensions;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddRabbitMqMassTransit();
builder.Services.AddDI();

var host = builder.Build();
await host.RunAsync();