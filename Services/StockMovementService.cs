using FiStockBackend.Context;
using FiStockBackend.Models;
using FiStockBackend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FiStockBackend.Services;
public class StockMovementService : IStockMovementService
{
    private readonly StockTrackingDbContext _context;

    public StockMovementService(StockTrackingDbContext context)
    {
        _context = context;
    }

    public async Task<StockMovement?> GetStockMovementByIdAsync(int id)
    {
        return await _context.StockMovements
            .Include(sm => sm.Product) // Ensure Product is not null
            .FirstOrDefaultAsync(sm => sm.StockMovementId == id);
    }

    public async Task<IEnumerable<StockMovement>> GetAllStockMovementsAsync()
    {
        return await _context.StockMovements
            .Include(sm => sm.Product!) // Ensure Product is not null
            .ToListAsync();
    }

    public async Task<StockMovement?> AddStockMovementAsync(StockMovement? stockMovement)
    {
        if (stockMovement == null)
        {
            return null;
        }

        _context.StockMovements.Add(stockMovement);
        await _context.SaveChangesAsync();
        return stockMovement;
    }

    public async Task<StockMovement?> UpdateStockMovementAsync(int id, StockMovement stockMovement)
    {
        var existingStockMovement = await GetStockMovementByIdAsync(id);
        if (existingStockMovement == null)
        {
            return null;
        }

        existingStockMovement.TransactionDate = stockMovement.TransactionDate;
        existingStockMovement.TransactionType = stockMovement.TransactionType;
        existingStockMovement.ProductCode = stockMovement.ProductCode;
        existingStockMovement.Quantity = stockMovement.Quantity;
        existingStockMovement.UnitPrice = stockMovement.UnitPrice;
        existingStockMovement.TotalAmount = stockMovement.TotalAmount;
        existingStockMovement.SourceDestinationId = stockMovement.SourceDestinationId;
        existingStockMovement.Description = stockMovement.Description;
        existingStockMovement.Product = stockMovement.Product;
        await _context.SaveChangesAsync();
        return existingStockMovement;
    }

    public async Task<bool> DeleteStockMovementAsync(int id)
    {
        var stockMovement = await GetStockMovementByIdAsync(id);
        if (stockMovement == null)
        {
            return false;
        }

        _context.StockMovements.Remove(stockMovement);
        await _context.SaveChangesAsync();
        return true;
    }
}