using System.Net;
using App.Application.Features.Products.Create;
using App.Application.Features.Products.Dto;
using App.Application.Features.Products.Update;
using App.Application.Features.Products.UpdateStock;
using App.Application.Interfaces.Caching;
using App.Application.Interfaces.Persistence;
using App.Application.Interfaces.Persistence.Product;
using App.Domain.Entities;
using AutoMapper;

namespace App.Application.Features.Products;
//CLEAN KOD ANALİZİ
//Fast Fail ile önce olumsuz durum hedef alınarak hızlı dönül yapılır, edge case'ler her daim önce
//Guard clauses ile if() ile her şeyi yaz, sonra düzgünce kodu yaz!
//Cyclomatic complexity olabildiğince düşük olmalı
//Command, strategy design pattern, decorator, adaptor gibi araçlarla if clause'lardan kurtulunabilir
//Dinamik validasyonları (başka API'Ye gitme, veritabanına gitme vesaire) business katmanında yapıyoruz.
public class ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService) : IProductService
{
    private const string ProductListKey = "ProductList";
    public async Task<ServiceResult<List<ProductResponseDto>>> GetMostExpensiveProductsAsync(int count)
    {
        var products = await productRepository.GetMostExpensiveProductsAsync(count);
        
        // var productsAsDto = products.Select(product => new ProductResponseDto(product.Id, product.Name, product.Description, product.Price, product.Stock)).ToList();
        var productsAsDto = mapper.Map<List<ProductResponseDto>>(products);
        return ServiceResult<List<ProductResponseDto>>.Success(productsAsDto);
    }

    //null yerine boş liste dön. null dönme
    public async Task<ServiceResult<List<ProductResponseDto>>> GetAllProductAsync()
    {
        //Cache aside pattern
        //1. Cache içinde yoksa db içinde vardır, önce cache sonra db, 3. adımda da datayı cache içine at 

        var isCached = await cacheService.GetAsync<List<ProductResponseDto>>(ProductListKey);
        if (isCached is not null)
        {
            return ServiceResult<List<ProductResponseDto>>.Success(isCached);
        }
        
        var products = await productRepository.GetAllAsync();
        // var productsAsDto = products.Select(x => new ProductResponseDto(x.Id, x.Name, x.Description, x.Price, x.Stock)).ToList();
        var productsAsDto = mapper.Map<List<ProductResponseDto>>(products);
        //Cache içinde bulamadıki dbden getirip sonradan ekledik, 
        //BUNUN YERİNE DECORATOR VEYA PROXY DESIGN PATTERN İLE DAHA SAĞLIKLI KOD YAZILABİLİR
        await cacheService.AddAsync(ProductListKey, productsAsDto, TimeSpan.FromMinutes(1));
        return ServiceResult<List<ProductResponseDto>>.Success(productsAsDto);
    }

    public async Task<ServiceResult<List<ProductResponseDto>>> GetPagedProductsAsync(int pageNumber, int pageSize)
    {
        var products = await productRepository.GetAllPagedAsync(pageNumber, pageSize);
        var productsAsDto = products.Select(x => new ProductResponseDto(x.Id, x.Name, x.Description, x.Price, x.Stock,x.CategoryId)).ToList();
        return ServiceResult<List<ProductResponseDto>>.Success(productsAsDto);
    }

    public async Task<ServiceResult<ProductResponseDto?>> GetProductByIdAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);
        if (product == null)
        {
            return ServiceResult<ProductResponseDto?>.Fail("Product not found",HttpStatusCode.NotFound);
        }
        var productAsDto = new ProductResponseDto(product.Id, product.Name, product.Description, product.Price, product.Stock, product.CategoryId);
        return ServiceResult<ProductResponseDto>.Success(productAsDto)!;
    }

    //201 dönülürse, oluşturulan nesneye nasıl gidileceği response içinde dönülebilir. URL verilir.
    public async Task<ServiceResult<ProductCreateResponseDto>> CreateProductAsync(ProductCreateRequestDto productCreateRequestDto)
    {
        //SERVICE METODUNDA VALIDATION, EN KOLAY VE EN PERFORMANSLI 
        var isInDb = await productRepository.AnyAsync(x => x.Name == productCreateRequestDto.Name);
        if (isInDb)
        {
            return ServiceResult<ProductCreateResponseDto>.Fail("Product already exist as same name");
        }
        var product = mapper.Map<Product>(productCreateRequestDto);
        await productRepository.AddAsync(product);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult<ProductCreateResponseDto>.SuccessAsCreated(new ProductCreateResponseDto(product.Id), $"api/product/{product.Id}");
    }

    public async Task<ServiceResult> UpdateProductAsync(int id, ProductUpdateRequestDto productUpdateRequestDto)
    {
        var product = await productRepository.GetByIdAsync(id);
        if (product is null)
        {
            return ServiceResult.Fail("Product not found");
        }
        var isInDb = await productRepository.AnyAsync(x => x.Name == productUpdateRequestDto.Name && x.Id != product.Id);
        if (isInDb)
        {
            return ServiceResult.Fail("Product already exist as same name");
        }
        //jenerik değil, o yüzden normal parametre olarak verilebilir.
        productRepository.Update(product);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult> UpdateStockAsync(ProductUpdateStockRequestDto productUpdateStockRequestDto)
    {
        var product = await productRepository.GetByIdAsync(productUpdateStockRequestDto.ProductId);
        if (product is null)
        {
            return ServiceResult.Fail("Product not found");
        }
        product.Stock = productUpdateStockRequestDto.Quantity;
        productRepository.Update(product);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult> DeleteProductAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);
        if (product is null)
        {
            return ServiceResult.Fail("Product not found");
        }
        productRepository.Delete(product);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}