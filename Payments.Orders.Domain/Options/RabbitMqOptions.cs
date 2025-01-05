namespace Payments.Orders.Domain.Options;

public class RabbitMqOptions
{
    public string Hostname { get; set; } = null!;
    public int Port { get; set; }
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string VirtualHost { get; set; } = null!;
    public string CreateOrderQueueName { get; set; } = null!;
}
