using Newtonsoft.Json;

namespace PropertyTechCase.Event.Api.Model.Configuration;

public class Config
{
    [JsonProperty("RabbitMqSetting")] public RabbitMqSetting RabbitMqSetting { get; }
}

public class RabbitMqSetting
{
    [JsonProperty("HostName")] public string HostName { get; }

    [JsonProperty("Username")] public string Username { get; }

    [JsonProperty("Password")] public string Password { get; }
}