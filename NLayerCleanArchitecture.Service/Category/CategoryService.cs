using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLayerCleanArchitecture.Repository;
using NLayerCleanArchitecture.Repository.Category;
using NLayerCleanArchitecture.Service.Category.Create;
using NLayerCleanArchitecture.Service.Category.Update;
using NLayerCleanArchitecture.Service.Products.Create;

namespace NLayerCleanArchitecture.Service.Category;

public class CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork,IMapper mapper) : ICategoryService
{
    //crud operations
    public async Task<ServiceResult<CategoryResponseDto>> GetCategoryById(int id)
    {
        var entity = await categoryRepository.GetByIdAsync(id);
        if (entity is null)
        {
            return ServiceResult<CategoryResponseDto>.Fail("Category not found");
        }
        return ServiceResult<CategoryResponseDto>.Success(mapper.Map<CategoryResponseDto>(entity));
    }
    public async Task<ServiceResult<List<CategoryResponseDto>>> GetPagedCategoriesAsync(int pageNumber, int pageSize)
    {
        var entities = await categoryRepository.GetAll()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var categoryDtos = mapper.Map<List<CategoryResponseDto>>(entities);

        return ServiceResult<List<CategoryResponseDto>>.Success(categoryDtos);
    }
    public async Task<ServiceResult<List<CategoryResponseDto>>> GetAllCategoriesAsync()
    {
        var entities = await categoryRepository.GetAll().ToListAsync();
        return ServiceResult<List<CategoryResponseDto>>.Success(mapper.Map<List<CategoryResponseDto>>(entities));
    }
    public async Task<ServiceResult<CategoryWithProductResponseDto>> GetCategoryWithProductsAsync(int id)
    {
        var entity = await categoryRepository.GetCategoryWithProducts(id);
        if (entity is null)
        {
            return ServiceResult<CategoryWithProductResponseDto>.Fail("Category not found");
        }
        return ServiceResult<CategoryWithProductResponseDto>.Success(mapper.Map<CategoryWithProductResponseDto>(entity));
    }
    public async Task<ServiceResult<List<CategoryWithProductResponseDto>>> GetAllCategoriesWithProductsAsync()
    {
        var entities = await categoryRepository.GetCategoryWithProducts().ToListAsync();
        return ServiceResult<List<CategoryWithProductResponseDto>>.Success(mapper.Map<List<CategoryWithProductResponseDto>>(entities));
    }
    public async Task<ServiceResult<CategoryCreateResponseDto>> CreateCategoryAsync(CategoryCreateRequestDto categoryCreateRequestDto)
    { 
        var isInDb = await categoryRepository.Where(x => x.Name == categoryCreateRequestDto.Name).AnyAsync();
        if (isInDb)
        {
            return ServiceResult<CategoryCreateResponseDto>.Fail("Product already exist as same name");
        }
        var entity = mapper.Map<Repository.Category.Category>(categoryCreateRequestDto);
        await categoryRepository.AddAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult<CategoryCreateResponseDto>.SuccessAsCreated(new CategoryCreateResponseDto(entity.Id), $"api/product/{entity.Id}");
    }
    public async Task<ServiceResult> UpdateCategoryAsync(int id, CategoryUpdateRequestDto categoryUpdateRequestDto)
    {
        var entity = await categoryRepository.GetByIdAsync(id);
        if (entity is null)
        {
            return ServiceResult.Fail("entity not found");
        }

        var isAliasInDb = await categoryRepository.Where(x => x.Name == categoryUpdateRequestDto.Name && x.Id != id).AnyAsync();
        if (isAliasInDb)
        {
            return ServiceResult.Fail("There is an entity exist with a same name in database. Please change the name");
        }
        entity.Name = categoryUpdateRequestDto.Name;
        categoryRepository.Update(entity);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
    public async Task<ServiceResult> DeleteCategoryAsync(int id)
    {
        var product = await categoryRepository.GetByIdAsync(id);
        if (product is null)
        {
            return ServiceResult.Fail("Product not found");
        }
        categoryRepository.Delete(product);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}