using AutoMapper;
using NLayerCleanArchitecture.Repository.Products;
using NLayerCleanArchitecture.Repository.Category;
using NLayerCleanArchitecture.Service.Category;
using NLayerCleanArchitecture.Service.Category.Create;
using NLayerCleanArchitecture.Service.Category.Update;
using NLayerCleanArchitecture.Service.Products;
using NLayerCleanArchitecture.Service.Products.Create;
using NLayerCleanArchitecture.Service.Products.Update;

namespace NLayerCleanArchitecture.Service.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Repository.Category.Category, CategoryResponseDto>();
        CreateMap<Repository.Category.Category, CategoryWithProductResponseDto>();
        CreateMap<CategoryCreateRequestDto, Repository.Category.Category>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));
        CreateMap<CategoryUpdateRequestDto, Repository.Category.Category>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));

        CreateMap<ProductResponseDto, Product>().ReverseMap();
        //sen dto'nun name'ini küçük yap ve hedefe öyle maple
        CreateMap<ProductCreateRequestDto, Product>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));
        //sen dto'nun name'ini küçük yap ve hedefe öyle maple
        CreateMap<ProductUpdateRequestDto, Product>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));
    }
    
}