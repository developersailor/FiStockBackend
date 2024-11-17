using FiStockBackend.Models;
using FiStockBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FiStockBackend.Controllers;
[Route("api/products")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] Product? product)
    {
        if (product == null)
        {
            return BadRequest("Product cannot be null.");
        }

        var createdProduct = await _productService.CreateProductAsync(product);
        if (createdProduct == null)
        {
            return StatusCode(500, "A problem happened while handling your request.");
        }
        return CreatedAtAction(nameof(GetProductById), new { productCode = createdProduct.ProductCode }, createdProduct);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _productService.GetAllProductsAsync();
        return Ok(products);
    }

    [HttpGet("{productCode}")]
    public async Task<IActionResult> GetProductById(int productCode)
    {
        var product = await _productService.GetProductByIdAsync(productCode);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpPut("{productCode}")]
    public async Task<IActionResult> UpdateProduct(int productCode, [FromBody] Product? product)
    {
        if (product == null || productCode != int.Parse(product.ProductCode))
        {
            return BadRequest();
        }

        await _productService.UpdateProductAsync(product);
        return NoContent();
    }

    [HttpDelete("{productCode}")]
    public async Task<IActionResult> DeleteProduct(int productCode)
    {
        await _productService.DeleteProductAsync(productCode);
        return NoContent();
    }
}