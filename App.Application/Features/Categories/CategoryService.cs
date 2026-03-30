using System.Net;
using App.Application.Features.Categories.Create;
using App.Application.Features.Categories.Dto;
using App.Application.Features.Categories.Update;
using App.Application.Interfaces.Persistence;
using App.Application.Interfaces.Persistence.Category;
using App.Domain.Entities;
using AutoMapper;

namespace App.Application.Features.Categories;

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
        var entities = await categoryRepository.GetAllPagedAsync(pageNumber, pageSize);

        var categoryDtos = mapper.Map<List<CategoryResponseDto>>(entities);

        return ServiceResult<List<CategoryResponseDto>>.Success(categoryDtos);
    }
    public async Task<ServiceResult<List<CategoryResponseDto>>> GetAllCategoriesAsync()
    {
        var entities = await categoryRepository.GetAllAsync();
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
        var entities = await categoryRepository.GetAllCategoriesWithTheirProductsAsync();
        return ServiceResult<List<CategoryWithProductResponseDto>>.Success(mapper.Map<List<CategoryWithProductResponseDto>>(entities));
    }
    public async Task<ServiceResult<CategoryCreateResponseDto>> CreateCategoryAsync(CategoryCreateRequestDto categoryCreateRequestDto)
    { 
        var isInDb = await categoryRepository.AnyAsync(x => x.Name == categoryCreateRequestDto.Name);
        if (isInDb)
        {
            return ServiceResult<CategoryCreateResponseDto>.Fail("Category already exists with the same name");
        }
        var entity = mapper.Map<Category>(categoryCreateRequestDto);
        await categoryRepository.AddAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult<CategoryCreateResponseDto>.SuccessAsCreated(new CategoryCreateResponseDto(entity.Id), $"api/categories/{entity.Id}");
    }
    public async Task<ServiceResult> UpdateCategoryAsync(int id, CategoryUpdateRequestDto categoryUpdateRequestDto)
    {
        var entity = await categoryRepository.GetByIdAsync(id);
        if (entity is null)
        {
            return ServiceResult.Fail("entity not found");
        }

        var isAliasInDb = await categoryRepository.AnyAsync(x => x.Name == categoryUpdateRequestDto.Name && x.Id != id);
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
        var category = await categoryRepository.GetByIdAsync(id);
        if (category is null)
        {
            return ServiceResult.Fail("Category not found");
        }
        categoryRepository.Delete(category);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}