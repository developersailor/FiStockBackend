namespace FiStockBackend.DTO;

public class ProductDTO
{
    public int ProductId { get; set; }
    public string ProductCode { get; set; }
    public string Barcode { get; set; }
    public string ProductName { get; set; }
    public string SKU { get; set; }
    public string Unit { get; set; }
    public decimal UnitPrice { get; set; }
    public int SupplierId { get; set; }
    public int CategoryId { get; set; }
}