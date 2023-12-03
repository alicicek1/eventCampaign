public class Config
{
    public RabbitMqSetting RabbitMqSetting { get; set; }
}

public class RabbitMqSetting
{
    public string HostName { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }
}