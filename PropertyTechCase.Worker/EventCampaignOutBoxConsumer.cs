using System.Text.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PropertyTechCase.Database;

namespace PropertyTechCase.ConsumerWorker;

public class EventCampaignOutBoxConsumer : BackgroundService
{
    private readonly ILogger<EventCampaignOutBoxConsumer> _logger;
    private readonly IDatabase _database;

    public EventCampaignOutBoxConsumer(ILogger<EventCampaignOutBoxConsumer> logger, IDatabase database)
    {
        _logger = logger;
        _database = database;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            IEnumerable<EventCampaignOutBox> list = _database.GetCampaignEventOutboxData();

            foreach (var eventCampaignOutBox in list)
            {
                EventCampaignEntity? deptObj =
                    JsonSerializer.Deserialize<EventCampaignEntity>(eventCampaignOutBox.Payload);

                if (deptObj != null) _database.AddToEventCampaign(deptObj);
                
                
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}