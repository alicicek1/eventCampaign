using Microsoft.Extensions.Caching.Memory;
using PropertyTechCase.Database;
using PropertyTechCase.Event.Api.Middlewares;
using PropertyTechCase.Event.Api.Model.Configuration;
using PropertyTechCase.Event.Api.MvcFilters;
using PropertyTechCase.Event.Api.RabbitMq;
using PropertyTechCase.Event.Api.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var appSettingsSection = builder.Configuration.GetSection("Config");

builder.Services.Configure<Config>(appSettingsSection);

builder.Services.AddOptions();
builder.Services.AddSingleton<IDatabase, Database>();

builder.Services.AddScoped<IEventCampaignService, EventCampaignService>();
builder.Services.AddScoped<IRabbitMqProducer, RabbitMqProducer>();

builder.Services.AddScoped<ResponseLoggingActionAttribute>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMiddleware<ExceptionMiddleware>();
    app.UseMiddleware<CorrelationIdMiddleware>();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();