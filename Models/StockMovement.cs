namespace FiStockBackend.Models;

public class StockMovement
{
    public int StockMovementId { get; set; }
    public DateTime TransactionDate { get; set; }
    public int ProductId { get; set; } // Foreign key
    public Product Product { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalAmount { get; set; }
    public int SourceDestinationId { get; set; }
    public Warehouse SourceDestination { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string TransactionType { get; set; } = null!;
}