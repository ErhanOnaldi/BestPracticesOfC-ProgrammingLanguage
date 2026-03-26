using FluentValidation;

namespace NLayerCleanArchitecture.Service.Category.Create;

public class CategoryCreateRequestValidator : AbstractValidator<CategoryCreateRequestDto>
{
    public CategoryCreateRequestValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Name is required").MaximumLength(50).WithMessage("Name must not exceed 50 characters");
    }
}