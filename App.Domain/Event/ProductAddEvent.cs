namespace App.Domain.Event;

public record ProductAddEvent(int Id, string Name, decimal Price);