namespace FiStockBackend.Models;

public class Customer
{
    public int CustomerCode { get; set; } // Primary Key
    public string CustomerName { get; set; }
    public string ContactInformation { get; set; }
}