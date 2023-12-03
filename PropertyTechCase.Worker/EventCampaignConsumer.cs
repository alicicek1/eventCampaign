using System.Text;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace PropertyTechCase.ConsumerWorker;

public class EventCampaignConsumer : BackgroundService
{
    private readonly ILogger<EventCampaignConsumer> _logger;
    private readonly IRabbitMqConnectionService _connectionService;

    public EventCampaignConsumer(ILogger<EventCampaignConsumer> logger, IRabbitMqConnectionService connectionService)
    {
        _logger = logger;
        _connectionService = connectionService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            using var channel = _connectionService.CreateModel();
            channel.QueueDeclare("campaignEvent", exclusive: false);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"campaignEvent message received: {message}");
            };
            channel.BasicConsume(queue: "campaignEvent", autoAck: true, consumer: consumer);
            Console.ReadKey();

            await Task.Delay(1000, stoppingToken);
        }
    }
}