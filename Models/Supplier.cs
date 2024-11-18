namespace FiStockBackend.Models;

public class Supplier
{
    public int SupplierId { get; set; }
    public required string SupplierCode { get; set; }
    public required string SupplierName { get; set; }
    public required string ContactInformation { get; set; }
}