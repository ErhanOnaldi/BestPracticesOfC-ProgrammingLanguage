using App.Application.Interfaces.ServiceBus;
using App.Domain.Event;
using MassTransit;

namespace App.Bus;

public class ServiceBus(IPublishEndpoint publishEndpoint, ISendEndpointProvider sendEndpointProvider) : IServiceBus
{
    public async Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : IMessage
    {
        await publishEndpoint.Publish(message, cancellationToken);
    }

    public async Task SendAsync<T>(T message, string queueName, CancellationToken cancellationToken = default) where T : IMessage
    {
        var endpoint = await sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{queueName}"));
        await endpoint.Send(message, cancellationToken);
    }
}