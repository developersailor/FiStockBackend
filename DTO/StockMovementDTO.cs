namespace FiStockBackend.DTO;

public class StockMovementDTO
{
    public int StockMovementId { get; set; }
    public DateTime TransactionDate { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalAmount { get; set; }
    public int SourceDestinationId { get; set; }
    public string Description { get; set; }
    public string TransactionType { get; set; }
}