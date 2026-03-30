using App.Application.Features.Categories.Create;
using App.Application.Features.Categories.Dto;
using App.Application.Features.Categories.Update;

namespace App.Application.Features.Categories;

public interface ICategoryService
{
    Task<ServiceResult<CategoryResponseDto>> GetCategoryById(int id);
    Task<ServiceResult<List<CategoryResponseDto>>> GetPagedCategoriesAsync(int pageNumber, int pageSize);
    Task<ServiceResult<List<CategoryResponseDto>>> GetAllCategoriesAsync();
    Task<ServiceResult<CategoryWithProductResponseDto>> GetCategoryWithProductsAsync(int id);
    Task<ServiceResult<List<CategoryWithProductResponseDto>>> GetAllCategoriesWithProductsAsync();
    Task<ServiceResult<CategoryCreateResponseDto>> CreateCategoryAsync(CategoryCreateRequestDto categoryCreateRequestDto);
    Task<ServiceResult> UpdateCategoryAsync(int id, CategoryUpdateRequestDto categoryUpdateRequestDto);
    Task<ServiceResult> DeleteCategoryAsync(int id);

}