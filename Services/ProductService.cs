using FiStockBackend.Context;
using FiStockBackend.Models;
using FiStockBackend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FiStockBackend.Services;
public class ProductService : IProductService
{
    private readonly StockTrackingDbContext _context;

    public ProductService(StockTrackingDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> CreateProductAsync(Product? product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product));
        }

        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product?> GetProductByIdAsync(int productCode)
    {
        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .FirstOrDefaultAsync(p => p.ProductCode == productCode.ToString());
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .ToListAsync();
    }

    public async Task UpdateProductAsync(Product? product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product));
        }

        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int productCode)
    {
        var product = await GetProductByIdAsync(productCode);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Product>> GetProductsBySupplierIdAsync(int supplierId)
    {
        return await _context.Products
            .Include(p => p.Supplier)
            .Where(p => p.SupplierId == supplierId)
            .ToListAsync();
    }
}