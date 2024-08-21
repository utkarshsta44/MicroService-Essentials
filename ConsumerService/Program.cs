using Confluent.Kafka;
using ConsumerService;
using ConsumerService.Application.Commands;
using ConsumerService.Application.Handlers;
using ConsumerService.Application.Interfaces;
using ConsumerService.Infrastructure.Messaging;
using ConsumerService.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();


// Register MediatR and application services
builder.Services.AddMediatR(typeof(ConsumeEmployeeCommandHandler).Assembly);
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

// Register the DbContext with SQL Server
builder.Services.AddDbContext<ConsumerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Kafka Consumer
builder.Services.AddSingleton(new ConsumerConfig
{
    BootstrapServers = builder.Configuration["Kafka:BootstrapServers"],
    GroupId = "consumer-group-1",
    AutoOffsetReset = AutoOffsetReset.Earliest
});
builder.Services.AddHostedService<KafkaConsumerService>();

var host = builder.Build();
host.Run();
