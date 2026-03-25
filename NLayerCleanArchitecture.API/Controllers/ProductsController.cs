using Microsoft.AspNetCore.Mvc;
using NLayerCleanArchitecture.Service.Products;

//Endpoint name api/products şeklinde olmalı.
namespace NLayerCleanArchitecture.API.Controllers;
//Root constraints: "{pageNumber:int}/{pageSize:int}" buradaki değerlere uymayan tüm girdilere ototmatik 404 dönülebilir.
public class ProductsController(IProductService productService) : CustomBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll() => CreateActionResult(await productService.GetAllProductAsync());
    
    [HttpGet("{pageNumber:int}/{pageSize:int}")]
    public async Task<IActionResult> GetPagedAll(int pageNumber, int pageSize) => CreateActionResult(await productService.GetPagedProductsAsync(pageNumber, pageSize));
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id) => CreateActionResult(await productService.GetProductByIdAsync(id));
    
    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateRequestDto productCreateRequestDto) => CreateActionResult(await productService.CreateProductAsync(productCreateRequestDto));
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, ProductUpdateRequestDto productUpdateRequestDto) => CreateActionResult(await productService.UpdateProductAsync(id, productUpdateRequestDto));

    [HttpPatch("stock")]
    public async Task<IActionResult> UpdateStockAsync(ProductUpdateStockRequestDto productUpdateStockRequestDto) => CreateActionResult(await productService.UpdateStockAsync(productUpdateStockRequestDto));
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) => CreateActionResult(await productService.DeleteProductAsync(id));
}