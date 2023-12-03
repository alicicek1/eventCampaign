using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace PropertyTechCase.ConsumerWorker;

public interface IRabbitMqConnectionService
{
    IModel CreateModel();
}

public class RabbitMqConnectionService : IRabbitMqConnectionService
{
    private readonly IConnection _connection;
    private readonly IOptions<Config> _configSettings;

    public RabbitMqConnectionService(IOptions<Config> configSettings)
    {
        _configSettings = configSettings;
        var factory = new ConnectionFactory
        {
            HostName = configSettings.Value.RabbitMqSetting.HostName,
            UserName = configSettings.Value.RabbitMqSetting.Username,
            Password = configSettings.Value.RabbitMqSetting.Password,
        };

        _connection = factory.CreateConnection();
    }

    public IModel CreateModel()
    {
        return _connection.CreateModel();
    }
}