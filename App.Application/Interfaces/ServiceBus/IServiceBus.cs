using App.Domain.Event;

namespace App.Application.Interfaces.ServiceBus;

public interface IServiceBus
{
    Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : IMessage;
    Task SendAsync<T>(T message,string queueName, CancellationToken cancellationToken = default) where T : IMessage;
    
}