namespace FiStockBackend.Models;

public class Customer
{
    public int CustomerId { get; set; }
    public int CustomerCode { get; set; } // Primary Key
    public required string CustomerName { get; set; }
    public required string ContactInformation { get; set; }
}