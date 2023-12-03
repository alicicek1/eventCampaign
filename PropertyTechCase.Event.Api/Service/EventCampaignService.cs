using Microsoft.Extensions.Caching.Memory;
using PropertyTechCase.Database;
using PropertyTechCase.Event.Api.Model.Results;
using PropertyTechCase.Event.Api.RabbitMq;
using EventCampaignEntity = PropertyTechCase.Event.Api.Model.EventCampaign.EventCampaignEntity;

namespace PropertyTechCase.Event.Api.Service;

public class EventCampaignService : IEventCampaignService
{
    private readonly IRabbitMqProducer _rabbitMqProducer;
    private readonly IDatabase _database;

    public EventCampaignService(IRabbitMqProducer rabbitMqProducer, IDatabase database)
    {
        _rabbitMqProducer = rabbitMqProducer;
        _database = database;
    }

    public DataResult<string?> PublishEvent(EventCampaignEntity entity)
    {
        try
        {
            _rabbitMqProducer.PublishEventCampaignMessage(entity);
        }
        catch (Exception)
        {
            _database.AddToEventCampaignOutbox(new EventCampaignOutBox
            {
                OccuredOn = DateTime.Now,
                ProcessedDate = null,
                Type = "null",
                Payload = Newtonsoft.Json.JsonConvert.SerializeObject(entity),
                IdempotentToken = default
            });
        }

        return new SuccessDataResult<string?>(null, "Event successfully published.");
    }
}