namespace FiStockBackend.Models;

public class Product
{
    public int ProductId { get; set; }
    public required string ProductCode { get; set; }
    public int SupplierId { get; set; }
    public Supplier Supplier { get; set; } = null!;
    public required string Barcode { get; set; }
    public required string ProductName { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();
    public required string Unit { get; set; }

    public required string SKU { get; set; }

}