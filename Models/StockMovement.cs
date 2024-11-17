namespace FiStockBackend.Models;
public class StockMovement
{
    public int Id { get; set; }
    public int StockMovementId { get; set; }
    public DateTime TransactionDate { get; set; }
    public string ProductCode { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalAmount { get; set; }
    public int SourceDestinationId { get; set; }
    public string Description { get; set; } = null!;
    public Product Product { get; set; } = null!;
    public string TransactionType { get; set; } = null!;
}