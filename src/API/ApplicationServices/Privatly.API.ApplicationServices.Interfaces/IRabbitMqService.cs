namespace Privatly.API.ApplicationServices.Interfaces;

public interface IRabbitMqService
{
    IEnumerable<string> AvailableQueues { get; }

    Task Post(object? data, string queueName);
}