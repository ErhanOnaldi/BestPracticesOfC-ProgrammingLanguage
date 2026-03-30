using App.Domain.Event;
using MassTransit;

namespace App.Bus.Consumer;

public class ProductAddEventConsumer : IConsumer<ProductAddEvent>
{
    public Task Consume(ConsumeContext<ProductAddEvent> context)
    {
        Console.WriteLine($"message incoming: {context.Message.Id} - {context.Message.Name} - {context.Message.Price} ");
        return Task.CompletedTask;
    }
}
