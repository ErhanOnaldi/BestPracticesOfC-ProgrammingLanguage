using FluentValidation;

namespace App.Application.Features.Categories.Update;

public class CategoryUpdateRequestValidator : AbstractValidator<CategoryUpdateRequestDto>
{
    public CategoryUpdateRequestValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Name is required").MaximumLength(50).WithMessage("Name must not exceed 50 characters");
    }
}