using Microsoft.AspNetCore.Mvc;
using NLayerCleanArchitecture.Service.Products;

namespace NLayerCleanArchitecture.API.Controllers;

public class ProductsController(IProductService productService) : CustomBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll() => CreateActionResult(await productService.GetAllProductAsync());
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id) => CreateActionResult(await productService.GetProductByIdAsync(id));
    
    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateRequestDto productCreateRequestDto) => CreateActionResult(await productService.CreateProductAsync(productCreateRequestDto));
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProductUpdateRequestDto productUpdateRequestDto) => CreateActionResult(await productService.UpdateProductAsync(id, productUpdateRequestDto));
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) => CreateActionResult(await productService.DeleteProductAsync(id));
}