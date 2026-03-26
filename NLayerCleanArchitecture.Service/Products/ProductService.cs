using System.Net;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NLayerCleanArchitecture.Repository;
using NLayerCleanArchitecture.Repository.Products;
using NLayerCleanArchitecture.Service.Products.Create;
using NLayerCleanArchitecture.Service.Products.Update;
using NLayerCleanArchitecture.Service.Products.UpdateStock;

namespace NLayerCleanArchitecture.Service.Products;
//CLEAN KOD ANALİZİ
//Fast Fail ile önce olumsuz durum hedef alınarak hızlı dönül yapılır, edge case'ler her daim önce
//Guard clauses ile if() ile her şeyi yaz, sonra düzgünce kodu yaz!
//Cyclomatic complexity olabildiğince düşük olmalı
//Command, strategy design pattern, decorator, adaptor gibi araçlarla if clause'lardan kurtulunabilir
//Dinamik validasyonları (başka API'Ye gitme, veritabanına gitme vesaire) business katmanında yapıyoruz.
public class ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper) : IProductService
{
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
        var products = await productRepository.GetAll().ToListAsync();
        // var productsAsDto = products.Select(x => new ProductResponseDto(x.Id, x.Name, x.Description, x.Price, x.Stock)).ToList();
        var productsAsDto = mapper.Map<List<ProductResponseDto>>(products);
        return ServiceResult<List<ProductResponseDto>>.Success(productsAsDto);
    }

    public async Task<ServiceResult<List<ProductResponseDto>>> GetPagedProductsAsync(int pageNumber, int pageSize)
    {
        var products = await productRepository.GetAll().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
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
        var isInDb = await productRepository.Where(x => x.Name == productCreateRequestDto.Name).AnyAsync();
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
        var isInDb = await productRepository.Where(x => x.Name == productUpdateRequestDto.Name && x.Id != product.Id).AnyAsync();
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