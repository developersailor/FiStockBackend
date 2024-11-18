
namespace FiStockBackend.Services.Interfaces;

using FiStockBackend.Models;

public interface IProductService
{
    Task<Product?> GetProductByIdAsync(int productCode);
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product?> CreateProductAsync(Product? product);
    Task UpdateProductAsync(Product? product);
    Task DeleteProductAsync(int productCode);
    Task<IEnumerable<Product>> GetProductsBySupplierIdAsync(int supplierId);
}