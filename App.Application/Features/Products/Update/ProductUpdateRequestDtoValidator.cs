using FluentValidation;

namespace App.Application.Features.Products.Update;

public class ProductUpdateRequestDtoValidator : AbstractValidator<ProductUpdateRequestDto>
{
    public ProductUpdateRequestDtoValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage("Description is required");
        RuleFor(x => x.Price).NotNull().NotEmpty();
        RuleFor(x => x.Price).InclusiveBetween(0,100).NotNull();
        RuleFor(x => x.Stock).NotNull().NotEmpty();
        RuleFor(x => x.CategoryId).NotNull().NotEmpty();
    }
}