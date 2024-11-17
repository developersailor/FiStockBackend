namespace FiStockBackend.Models;

public class Supplier
{

    public int SupplierId { get; set; }
    public string SupplierCode { get; set; } = null!;
    public required string SupplierName { get; set; }
    public required string ContactInformation { get; set; }

}