using AutoMapper;
using NLayerCleanArchitecture.Repository.Products;
using NLayerCleanArchitecture.Service.Products;
using NLayerCleanArchitecture.Service.Products.Create;

namespace NLayerCleanArchitecture.Service.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProductResponseDto, Product>().ReverseMap();
    }
    
}