using FiStockBackend.Models;

namespace FiStockBackend.Services.Interfaces;

public interface IStockMovementService
{
    Task<StockMovement?> GetStockMovementByIdAsync(int id);
    Task<IEnumerable<StockMovement>> GetAllStockMovementsAsync();
    Task<StockMovement?> AddStockMovementAsync(StockMovement? stockMovement);
    Task<StockMovement?> UpdateStockMovementAsync(int id, StockMovement stockMovement); // Allow nullable return type
    Task<bool> DeleteStockMovementAsync(int id);
}