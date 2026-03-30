using App.Application.Interfaces.ServiceBus;
using App.Bus.Consumer;
using App.Domain.Constant;
using App.Domain.Options;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Bus;

public static class BusExtensions
{
    public static void AddBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IServiceBus, ServiceBus>();
        var serviceBusOption = configuration.GetSection(nameof(ServiceBusOption)).Get<ServiceBusOption>();
        services.AddMassTransit(x => x.UsingRabbitMq((context, cfg) =>
        {
            x.AddConsumer<ProductAddEventConsumer>();
            
            cfg.Host(new Uri(serviceBusOption!.Url), host => { });

            cfg.ReceiveEndpoint(ServiceBusConstants.ProductAddEventQueueName, ep =>
            {
                ep.ConfigureConsumer<ProductAddEventConsumer>(context);
            });
        }));
        
    }
}