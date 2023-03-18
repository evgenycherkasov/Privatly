using System.Text;
using System.Text.Json;
using Privatly.API.ApplicationServices.Interfaces;
using RabbitMQ.Client;

namespace Privatly.API.Infrastructure.RabbitMQ;

public class RabbitMqService : IRabbitMqService, IDisposable
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMqService(string hostName)
    {
        var factory = new ConnectionFactory { HostName = hostName };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        
        _channel.QueueDeclare(queue: "success_payment",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
    }

    public Task Post(object? data, string queueName)
    {
        var dataAsString = JsonSerializer.Serialize(data);

        return Task.Run(() => Post(dataAsString, queueName));
    }
    
    private void Post(string data, string queueName)
    {
        var body = Encoding.UTF8.GetBytes(data);

        _channel.BasicPublish(exchange: "",
            routingKey: queueName,
            basicProperties: null,
            body: body);
    }

    private void ReleaseUnmanagedResources()
    {
        _channel.Dispose();
        _connection.Dispose();
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~RabbitMqService()
    {
        ReleaseUnmanagedResources();
    }
}