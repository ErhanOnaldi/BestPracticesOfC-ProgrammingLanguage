namespace App.Application.Interfaces.ServiceBus;

public interface IServiceBus
{
    Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : class;
    
    
}