using FluentValidation;

namespace App.Application.Features.Products.Create;

public class ProductCreateRequestDtoValidator : AbstractValidator<ProductCreateRequestDto>
{
    public ProductCreateRequestDtoValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage("Description is required");
        RuleFor(x => x.Price).NotNull().NotEmpty();
        RuleFor(x => x.Price).InclusiveBetween(0,100).NotNull();
        RuleFor(x => x.Stock).NotNull().NotEmpty();
        RuleFor(x => x.CategoryId).NotNull().NotEmpty();
    }
    
    
    //BU TARZ DİNAMİK DURUMLARDA KULLANILAN FONKSİYON, VALİDASYONA MUST() METODU DİYE EKLENİR, FAKAT SENKRONDUR MAALESEF, ŞİRKET İÇİ KULLANIAM İDEAL
    // private bool MustUniqueProductName(string productName)
    // {
    //     return  !productRepository.Where(x => x.Name == name).Any();
    // }
}