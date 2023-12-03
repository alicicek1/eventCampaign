using Microsoft.Extensions.Caching.Memory;
using PropertyTechCase.Api.Middlewares;
using PropertyTechCase.Api.MvcFilters;
using PropertyTechCase.Api.Service;
using PropertyTechCase.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IMemoryCache, MemoryCache>();

builder.Services.AddSingleton<IDatabase, Database>();

builder.Services.AddScoped<ResponseLoggingActionAttribute>();

builder.Services.AddScoped<IEventCampaignService, EventCampaignService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMiddleware<ExceptionMiddleware>();
    app.UseMiddleware<CorrelationIdMiddleware>();
    app.UseMiddleware<AuthorizationMiddleware>();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();