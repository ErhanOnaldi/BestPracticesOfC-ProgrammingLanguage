using App.Application.Features.Categories;
using App.Application.Features.Categories.Create;
using App.Application.Features.Categories.Update;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;
//Root constraints: "{pageNumber:int}/{pageSize:int}" buradaki değerlere uymayan tüm girdilere ototmatik 404 dönülebilir.
public class CategoriesController(ICategoryService categoryService) : CustomBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll() => CreateActionResult(await categoryService.GetAllCategoriesAsync());
    
    [HttpGet("{pageNumber:int}/{pageSize:int}")]
    public async Task<IActionResult> GetPagedAll(int pageNumber, int pageSize) => CreateActionResult(await categoryService.GetPagedCategoriesAsync(pageNumber, pageSize));
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id) => CreateActionResult(await categoryService.GetCategoryById(id));

    [HttpGet("{id:int}/products")]
    public async Task<IActionResult> GetCategoryWithProducts(int id) => CreateActionResult(await categoryService.GetCategoryWithProductsAsync(id));
    
    [HttpGet("products")]
    public async Task<IActionResult> GetAllCategoriesWithProducts() => CreateActionResult(await categoryService.GetAllCategoriesWithProductsAsync());
    
    [HttpPost]
    public async Task<IActionResult> Create(CategoryCreateRequestDto categoryCreateRequestDto) => CreateActionResult(await categoryService.CreateCategoryAsync(categoryCreateRequestDto));
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, CategoryUpdateRequestDto categoryUpdateRequestDto) => CreateActionResult(await categoryService.UpdateCategoryAsync(id, categoryUpdateRequestDto));
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) => CreateActionResult(await categoryService.DeleteCategoryAsync(id));
}