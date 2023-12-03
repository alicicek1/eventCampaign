namespace PropertyTechCase.Event.Api.RabbitMq;

public interface IRabbitMqProducer
{
    public void PublishEventCampaignMessage<T>(T message);
    
}