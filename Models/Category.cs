namespace FiStockBackend.Models;


public class Category
{
    public int CategoryId { get; set; } // Primary Key
    public required string CategoryName { get; set; }
    public required List<Product> Products { get; set; } // Navigation Property
}