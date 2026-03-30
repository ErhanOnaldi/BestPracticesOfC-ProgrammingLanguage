namespace App.Application.Features.Products.Update;

public record ProductUpdateRequestDto(string Name, string? Description, decimal Price, int Stock, int CategoryId);