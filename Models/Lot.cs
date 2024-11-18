namespace FiStockBackend.Models;

public class Lot
{
    public int LotId { get; set; } // Primary key
    public required string LotNumber { get; set; }
    public DateTime ExpiryDate { get; set; }
    public int ProductId { get; set; } // Foreign key
    public Product Product { get; set; } = null!;
}