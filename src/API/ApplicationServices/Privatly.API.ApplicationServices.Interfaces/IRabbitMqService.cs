namespace Privatly.API.ApplicationServices.Interfaces;

public interface IRabbitMqService
{
    Task Post(object? data, string queueName);
}