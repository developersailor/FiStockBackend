namespace FiStockBackend.Models;

public class Customer
{
    public int CustomerId { get; set; } // Primary key
    public required string CustomerName { get; set; }
    public required string ContactInformation { get; set; }
}