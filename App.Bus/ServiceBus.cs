using App.Application.Interfaces.ServiceBus;

namespace App.Bus;

public class ServiceBus : IServiceBus
{
    public Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : class
    {
        throw new NotImplementedException();
    }
}