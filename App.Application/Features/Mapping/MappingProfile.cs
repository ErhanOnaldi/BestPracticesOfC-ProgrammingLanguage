using App.Application.Features.Categories.Create;
using App.Application.Features.Categories.Dto;
using App.Application.Features.Categories.Update;
using App.Application.Features.Products.Create;
using App.Application.Features.Products.Dto;
using App.Application.Features.Products.Update;
using App.Domain.Entities;
using AutoMapper;

namespace App.Application.Features.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Category, CategoryResponseDto>();
        CreateMap<Category, CategoryWithProductResponseDto>();
        CreateMap<CategoryCreateRequestDto, Category>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));
        CreateMap<CategoryUpdateRequestDto, Category>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));

        CreateMap<ProductResponseDto, Product>().ReverseMap();
        //sen dto'nun name'ini küçük yap ve hedefe öyle maple
        CreateMap<ProductCreateRequestDto, Product>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));
        //sen dto'nun name'ini küçük yap ve hedefe öyle maple
        CreateMap<ProductUpdateRequestDto, Product>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));
    }
    
}