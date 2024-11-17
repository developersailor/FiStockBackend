namespace FiStockBackend.Models;

public class Warehouse
{
    public int Id { get; set; }
    public required string WarehouseName { get; set; }
    public required string Address { get; set; }
    public required string ResponsiblePerson { get; set; }
    public required string WarehouseCode { get; set; }
}