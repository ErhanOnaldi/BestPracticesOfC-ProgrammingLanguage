using Microsoft.AspNetCore.Mvc;
using NLayerCleanArchitecture.Service.Products;

//Endpoint name api/products şeklinde olmalı.
namespace NLayerCleanArchitecture.API.Controllers;

public class ProductsController(IProductService productService) : CustomBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll() => CreateActionResult(await productService.GetAllProductAsync());
    
    [HttpGet("{pageNumber}/{pageSize}")]
    public async Task<IActionResult> GetPagedAll(int pageNumber, int pageSize) => CreateActionResult(await productService.GetPagedProductsAsync(pageNumber, pageSize));
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id) => CreateActionResult(await productService.GetProductByIdAsync(id));
    
    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateRequestDto productCreateRequestDto) => CreateActionResult(await productService.CreateProductAsync(productCreateRequestDto));
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProductUpdateRequestDto productUpdateRequestDto) => CreateActionResult(await productService.UpdateProductAsync(id, productUpdateRequestDto));

    [HttpPatch("stock")]
    public async Task<IActionResult> UpdateStockAsync(ProductUpdateStockRequestDto productUpdateStockRequestDto) => CreateActionResult(await productService.UpdateStockAsync(productUpdateStockRequestDto));
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) => CreateActionResult(await productService.DeleteProductAsync(id));
}