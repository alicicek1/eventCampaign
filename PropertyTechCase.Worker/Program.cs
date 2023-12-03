using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PropertyTechCase.ConsumerWorker;
using PropertyTechCase.Database;

var builder = Host.CreateApplicationBuilder(args);

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.Configure<Config>(configuration.GetSection("Config"));

builder.Services.AddScoped<IRabbitMqConnectionService, RabbitMqConnectionService>();

builder.Services.AddScoped<IDatabase, Database>();

builder.Services.AddHostedService<EventCampaignConsumer>();
builder.Services.AddHostedService<EventCampaignOutBoxConsumer>();


var host = builder.Build();
host.Run();