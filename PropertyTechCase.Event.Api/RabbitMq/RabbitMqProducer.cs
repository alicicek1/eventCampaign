using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PropertyTechCase.Event.Api.Model.Configuration;
using PropertyTechCase.Event.Api.Model.Constant;
using RabbitMQ.Client;

namespace PropertyTechCase.Event.Api.RabbitMq;

public class RabbitMqProducer : IRabbitMqProducer
{
    private readonly IModel _channel;
    private readonly IOptions<Config> _configSettings;

    public RabbitMqProducer(IOptions<Config> configSettings)
    {
        this._configSettings = configSettings;
        var factory = new ConnectionFactory
        {
            HostName = configSettings.Value.RabbitMqSetting.HostName,
            UserName = configSettings.Value.RabbitMqSetting.Username,
            Password = configSettings.Value.RabbitMqSetting.Password,
        };
        var connection = factory.CreateConnection();
        _channel = connection.CreateModel();
    }

    public void PublishEventCampaignMessage<T>(T message)
    {
        _channel.QueueDeclare(Constants.EventCampaignQueueName, exclusive: false);
        var json = JsonConvert.SerializeObject(message);
        var body = Encoding.UTF8.GetBytes(json);
        _channel.BasicPublish(exchange: "", routingKey: Constants.EventCampaignQueueName, body: body);
    }
}