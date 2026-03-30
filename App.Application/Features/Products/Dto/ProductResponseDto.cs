namespace App.Application.Features.Products.Dto;

public record ProductResponseDto(int Id, string Name, string? Description, decimal Price, int Stock, int CategoryId);
//mümjün olduğunca immutable olmalı

    // public int Id { get; init; }
    // public string Name { get; init; } = null!;
    // public string? Description { get; init; }
    // public decimal Price { get; init; }
    // public int Stock { get; init; }
