
namespace FiStockBackend.Services.Interfaces;

using FiStockBackend.Models; 
public interface IProductService
{
    Task<Product?> CreateProductAsync(Product? product);
    Task<Product> GetProductByIdAsync(int productCode);
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task UpdateProductAsync(Product? product);
    Task DeleteProductAsync(int productCode);
}